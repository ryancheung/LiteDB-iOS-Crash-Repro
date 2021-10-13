using LiteDB;

namespace iOSApp1;


public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string[] Phones { get; set; }
    public bool IsActive { get; set; }
}

[Register ("AppDelegate")]
public class AppDelegate : UIApplicationDelegate {
	public override UIWindow? Window {
		get;
		set;
	}

	public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
	{
		var dataRoot = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

		using(var db = new LiteDatabase(string.Format("Filename={0};Mode=Exclusive", Path.Combine(dataRoot, "1.db"))))
		{
			var col = db.GetCollection<Customer>("customers");

			var customer = new Customer
			{ 
				Name = "John Doe", 
				Phones = new string[] { "8000-0000", "9000-0000" }, 
				IsActive = true
			};
				
			// Crash in 5.X
			col.Insert(customer);
		}

		// create a new window instance based on the screen size
		Window = new UIWindow (UIScreen.MainScreen.Bounds);

		// create a UIViewController with a single UILabel
		var vc = new UIViewController ();
		vc.View!.AddSubview (new UILabel (Window!.Frame) {
			BackgroundColor = UIColor.White,
			TextAlignment = UITextAlignment.Center,
			Text = "Hello, iOS!"
		});
		Window.RootViewController = vc;

		// make the window visible
		Window.MakeKeyAndVisible ();

		return true;
	}
}
