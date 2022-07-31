package;

class Main
{
	static function main()
	{
		var a = 5;
		switch (a) 
		{
			case 5:
				trace(1);
			case 9:
				trace(2);
			default:
				trace("other");
		}
	}
}