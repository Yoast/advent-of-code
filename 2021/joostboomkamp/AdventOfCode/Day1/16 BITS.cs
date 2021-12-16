using Puzzles.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles;
public class BITS
{
    private static Dictionary<char, string> hexCharacterToBinary = new Dictionary<char, string> {
            { '0', "0000" },
            { '1', "0001" },
            { '2', "0010" },
            { '3', "0011" },
            { '4', "0100" },
            { '5', "0101" },
            { '6', "0110" },
            { '7', "0111" },
            { '8', "1000" },
            { '9', "1001" },
            { 'A', "1010" },
            { 'B', "1011" },
            { 'C', "1100" },
            { 'D', "1101" },
            { 'E', "1110" },
            { 'F', "1111" }
        };

    private static string HexStringToBinary(string hex)
    {
        StringBuilder result = new StringBuilder();
        foreach (char c in hex)
        {
            // This will crash for non-hex characters. You might want to handle that differently.
            result.Append(hexCharacterToBinary[char.ToUpper(c)]);
        }
        return result.ToString();
    }

    public static ICollection<IPacket> Parse(string hexInput)
    {
        var data = HexStringToBinary(hexInput);
        return BasePacket.Parse(data);
    }
}

public interface IPacket
{
    byte Version { get; }
    byte Type { get; }
    int BitsRead { get; }
    ICollection<IPacket> SubPackets { get; }
}

public abstract class BasePacket : IPacket
{
    protected const int HeaderSize = 6;

    public byte Version { get; set; }
    public byte Type { get; set; }
    public ICollection<IPacket> SubPackets { get; private set; } = new List<IPacket>();
    public int BitsRead { get; protected set; }
    //protected string Data { get; set; }

    //protected string _remainingData;
    //public string RemainingData
    //{
    //    get
    //    {
    //        var x = _remainingData;
    //        _remainingData = "";
    //        return x;
    //    }
    //    private set
    //    {
    //        _remainingData = value;
    //    }
    //}

    public static ICollection<IPacket> Parse(string binaryInput)
    {
        List<IPacket> packets = new List<IPacket>();

        var version = Convert.ToByte(binaryInput.Substring(0, 3), 2);
        var type = Convert.ToByte(binaryInput.Substring(3, 3), 2);
        var data = binaryInput.Substring(6);

        do
        {
            IPacket packet;
            switch (type)
            {
                case 4:
                    packet = new LiteralValuePacket(version, data); break;
                default:
                    packet = new OperatorPacket(version, type, data); break;
            }
            packets.Add(packet);

            data = binaryInput.Substring(packet.BitsRead);

        } while (data.Length > 0);

        return packets;
    }
}

public class LiteralValuePacket : BasePacket, IPacket
{
    public int Value { get; private set; }

    public LiteralValuePacket(byte version, string data)
    {
        // D2FE28
        // 110100101111111000101000
        // VVVTTTAAAAABBBBBcCCCC000

        //The three bits labeled V(110) are the packet version, 6.
        //The three bits labeled T(100) are the packet type ID, 4, which means the packet is a literal value.
        //The five bits labeled  A(10111) start with a 1(not the last group, keep reading) and contain the first four bits of the number, 0111.
        //The five bits labeled  B(11110) start with a 1(not the last group, keep reading) and contain four more bits of the number, 1110.
        //The five bits labeled  C(00101) start with a 0(last group, end of packet) and contain the last four bits of the number, 0101.
        //The three unlabeled 0 bits at the end are extra due to the hexadecimal representation and should be ignored.
        Version = version;
        Type = 4;
        
        var values = new StringBuilder();
        
        BitsRead = 0;
        var stop = false;
        do
        {
            var part = data.Substring(BitsRead, 5);
            stop = part[0] == '0';

            var value = part.Substring(1, 4);
            values.Append(value);

            BitsRead += 5;

        } while (!stop);

        // ignore the remaining bits, they should be 0 fills.
        while ( BitsRead % 4 > 0 )
        {
            BitsRead++;
        }

        // convert the output value
        Value = Convert.ToInt32(values.ToString(), 2);
    }
}

public class OperatorPacket: BasePacket, IPacket
{
    public OperatorPacket(byte version, byte type, string data)
    {
        Version = version;
        Type = type;

        int length = 0;

        var lengthType = data[0];
        BitsRead = 1;

        var remainingData = "";

        switch (lengthType)
        {
            case '0': // next 15 bits are length of subpackets
                var lengthStr = data.Substring(1, 15);
                length = Convert.ToInt32(lengthStr, 2);
                BitsRead += 15;

                var packetData = data.Substring(BitsRead, length);
                
                BitsRead += length;

                var serialPackets = Parse(packetData);
                SubPackets.AddRange(serialPackets);

                break;

            case '1': // next 11 bits are count of subpackets
                BitsRead += 11;
                var countStr = data.Substring(1, 11);
                var packetCount = Convert.ToInt32(countStr, 2);

                // parse N packets
                for (var i=0; i<packetCount; i++)
                {
                    var currentLength = data.Length;
                    var countedPackets = Parse(data[BitsRead..]);
                    SubPackets.AddRange(countedPackets);
                    BitsRead += countedPackets.Sum(p => p.BitsRead); // needs recursion
                }

                break;
        }
    }
}

