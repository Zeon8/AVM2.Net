package;

/**
 * ...
 * @author Zeon4
 */
extern class TestClass
{
	public function new();
	
	public static function testStatic(): Void;
	public static function testStaticWithReturn(number:Int): Int;
	
	public function test(): Void;
	public function testWithReturn(number:Int): Int;
}