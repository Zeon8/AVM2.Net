package ;
import classes.Class1;
import classes.Class3;
import classes.ClassWithArgs1;
import classes.ClassWithArgs2;
import classes.Class2;
/**
 * ...
 * @author Zeon4
 */
class ClassTests 
{

	public static function constructTest()
	{
		return new Class1();
	}
	
	public static function constructWithArgs(value: Int)
	{
		var obj = new ClassWithArgs1(value);
		return obj.a;
	}
	
	public static function constructSuperWithArgs(value: Int)
	{
		var obj = new ClassWithArgs2(value);
		return obj.a;
	}
	
	public static function callMethod(value: Int)
	{
		var obj = new Class1();
		return obj.test(value);
	}
	
	public static function callSuperMethod(value: Int)
	{
		var obj = new ClassWithArgs2(value);
		return obj.test();
	}
	
	public static function callOverridedMethod(value: Int)
	{
		var obj = new Class2();
		return obj.test(value);
	}
	
	public static function callOverridedMethodCallsSuper(value: Int)
	{
		var obj = new Class3();
		return obj.test(value);
	}
	
	public static function callStaticMethod(value: Int)
	{
		return Class1.staticTest(value);
	}
	
}