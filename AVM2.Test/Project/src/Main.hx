package;

class Main
{
	static function main()
	{
		var test: ITest = new Test();
		trace(Std.isOfType(test, IRandomInterface));
	}
}