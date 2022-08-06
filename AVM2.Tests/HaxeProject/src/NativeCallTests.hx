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
	
	public static function getStaticProperty() 
	{
		return TestClass.testStaticProperty;
	}
	
	public static function setStaticProperty(value: Int) 
	{
		TestClass.testStaticProperty = value;
	}
	
	public static function getProperty(test: TestClass) 
	{
		return test.testProperty;
	}
	
	public static function setProperty(test: TestClass, value: Int) 
	{
		test.testProperty = value;
	}
	
	
	
	
}