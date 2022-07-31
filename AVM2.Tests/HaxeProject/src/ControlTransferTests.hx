package ;

/**
 * ...
 * @author Zeon4
 */
class ControlTransferTests
{

	public static function ifTest(value: Bool)
	{
		var result:String;
		if (value)
			result = "true";
		else
			result = "false";
		return result;
	}
	
	public static function andCondition(one: Bool, two: Bool)
	{
		var result:String;
		if (one && two)
			result = "true";
		else
			result = "false";
		return result;
	}
	
	public static function orCondition(one: Bool, two: Bool)
	{
		var result:String;
		if (one || two)
			result = "true";
		else
			result = "false";
		return result;
	}
	
	public static function elseIf(num: Int)
	{
		var result:String;
		if (num == 1)
			result = "one";
		else if(num == 2)
			result = "two";
		else
			result = "other number";
		return result;
	}
	
	public static function switchTest(num: Int)
	{
		var result:String;
		switch (num) 
		{
			case 1:
				result = "one";
			case 2:
				result = "two";
			default:
				result = "other number";
		}
		return result;
	}
	
}