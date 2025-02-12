// Here you could define global logic that would affect all tests

// You can use attributes at the assembly level to apply to all tests in the assembly
[assembly: Retry(3)]

namespace Physics2DLibrary.TUnit;

public class GlobalHooks {
    [Before(TestSession)]
    public static async Task SetUp() {
        Console.WriteLine("Or you can define methods that do stuff before...");
    }

    [After(TestSession)]
    public static async Task CleanUp() {
        Console.WriteLine("...and after!");
    }
}