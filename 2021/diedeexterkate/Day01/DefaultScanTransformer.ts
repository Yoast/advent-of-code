import { ScanTransformer } from "./ScanTransformer";

export class DefaultScanTransformer implements ScanTransformer{
	public transformScans( scans:number[]): number[] {
		return scans;
	}
}
