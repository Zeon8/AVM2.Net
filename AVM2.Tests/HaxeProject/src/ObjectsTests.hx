package ;

/**
 * ...
 * @author Zeon4
 */
class ObjectsTests 
{

	public static function createArray()
	{
		var arr = [1, 2, 3, 4, 5];
		return arr;
	}
	
	public static function getArrayElement()
	{
		var arr = createArray();
		return arr[0];
	}
	
	public static function setArrayElement()
	{
		var arr = createArray();
		arr[0] = 2;
		return arr[0];
	}
	
	public static function createObject()
	{
		var obj = {
			a: 1,
			b: 2,
			c: 3
		};
		return obj;
	}
	
	public static function getObjectValue()
	{
		return createObject().a;
	}
	
	
	public static function setObjectValue(value:Int)
	{
		var obj = createObject();
		obj.a = value;
		return obj.a;
	}
	
	
}