package;

class NativeCallTests 
{

	public static function callStaticMethod() 
	{
		TestClass.testStatic();
	}
	
	public static function callStaticMethodWithReturn(number:Int) 
	{
		return TestClass.testStaticWithReturn(number);
	}
	
	public static function construct() 
	{
		return new TestClass();
	}
	
	public static function constructWithParams(number:Int) 
	{
		return new TestClass2(number);
	}
	
	public static function callMethod(test: TestClass) 
	{
		test.test();
	}
	
	public static function callMethodAndWithReturn(test: TestClass, number:Int) 
	{
		return test.testWithReturn(number);
	}
	
	
	
}