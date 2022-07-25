package ;
import classes.TestNestedClass;
import classes.TestNestedClass2;

/**
 * ...
 * @author Zeon4
 */
class NativeSuperCallTests 
{

	public static function construct()
	{
		return new TestNestedClass();
	}
	
	public static function constructWithParams(number:Int)
	{
		return new TestNestedClass2(number);
	}
	
	public static function callMethod()
	{
		var testClass = new TestNestedClass();
		testClass.test();
	}
	
	public static function callSuperMethod()
	{
		var testClass = new TestNestedClass();
		testClass.test();
	}
	
}