using Puzzles.Tools;

namespace Puzzles;

public static class InputDay12
{
	// 10
	public static string[] Example1 = @"start-A
start-b
A-c
A-b
b-d
A-end
b-end".SplitByLine();

	// 19
	public static string[] Example2 = @"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc".SplitByLine();

	// 226
	public static string[] Example3 = @"fs-end
he-DX
fs-he
start-DX
pj-DX
end-zg
zg-sl
zg-pj
pj-he
RW-he
fs-DX
pj-RW
zg-RW
start-pj
he-WI
zg-he
pj-fs
start-RW".SplitByLine();

	public static string [] Puzzle = @"zi-end
XR-start
zk-zi
TS-zk
zw-vl
zk-zw
end-po
ws-zw
TS-ws
po-TS
po-YH
po-xk
zi-ws
zk-end
zi-XR
XR-zk
vl-TS
start-zw
vl-start
XR-zw
XR-vl
XR-ws".SplitByLine();
}
