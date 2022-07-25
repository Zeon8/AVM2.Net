package classes;
import classes.Class1;

/**
 * ...
 * @author Zeon4
 */
class Class3 extends Class1
{

	public function new() 
	{
		super();
	}
	
	public override function test(value: Int)
	{
		return super.test(value) + value;
	}
	
}