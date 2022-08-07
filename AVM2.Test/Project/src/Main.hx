package;
import haxe.Exception;
import haxe.exceptions.ArgumentException;
import haxe.exceptions.NotImplementedException;

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
			trace(err.details());
		}
	}
	
	static function test()
	{
		throw new ArgumentException("test");
	}
}