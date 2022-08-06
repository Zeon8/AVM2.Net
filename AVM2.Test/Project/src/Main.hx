package;
import haxe.Exception;

class Main
{
	static function main()
	{
		try 
		{
			test();
		}
		catch (err:Exception)
		{
			trace(err.message);
		}
	}
	
	static function test()
	{
		throw new Exception("Test");
	}
}