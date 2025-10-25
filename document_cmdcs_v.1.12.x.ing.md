# CmdCs - Command Line Interpreter (v.1.12.x)

[_Turkish.v.1.12_](document_cmdcs_v.1.12.x.tr.md)

`CmdCs` is an advanced command-line tool that enhances the capabilities of the standard Windows command line (CMD) with C#-like syntax and can translate these commands into executable Batch (`.bat`) files. This project can function both as an interactive shell and directly convert C#-like code blocks, including loop and variable logic, into `.bat` scripts.

## ✨ Key Features

- **C#-like Syntax:** Offers a structure similar to C# for `for`, `while`, `do-while` loops, `int`, `string` variable declarations, and mathematical operations.
- **Batch File Generation:** Automatically translates your C#-like code into a valid `.bat` file using the `public class <FileName> : bat { ... }` syntax.
- **Variable Management:** Ability to use variables in `$variable` format, declare them with `set`, `int`, `string`, and perform mathematical operations.
- **Array Support:** Define arrays in `int[]` and `string[]` formats and iterate over them using `foreach` loops.
- **Conditional Commands:** Supports a `ternary` operator (`condition ? if_true : if_false`) to execute different commands based on the output of CMD commands.
- **Configuration File:** Manage initial settings (colors, notes, etc.) via the `Setting.cs` file.
- **Extensible Command Set:** Includes built-in utility commands like `help`, `hdd`.

## Configuration (Setting.txt)

You can customize the application's behavior by editing the `Setting.txt` file.

| Setting       | Example Value     | Description                                                                                                                                |
| ------------- | ----------------- | ------------------------------------------------------------------------------------------------------------------------------------------ |
| `linecolor`   | `Yellow,White`    | Sets the two alternating colors for the command prompt (`>>`).                                                                             |
| `defaultpage` | `Display1`        | Changes the startup message. `Display1` shows the English welcome message, while any other value (e.g., `Display2`) shows the Turkish one. |
| `note`        | `true` or `false` | When set to `true`, it displays additional notes during startup. When `false`, it hides them.                                              |

## 🚀 Usage

### 1. Interactive Mode

After running the program, you can type commands directly into the console to get immediate results. In this mode, commands are interpreted and executed on the C# side.

**Comment Line:**
```bash
# comment
// comment
```

**Variable Declaration and Usage:**

```bash
# Declare a numeric variable
int i = 10

# Print the variable's value to the screen
echo $i

# Declare a new variable with a mathematical operation
set j = $i * 2

# See the value of j
echo $j
```

**For Loop:**

```csharp
# Prints numbers from 0 to 4 to the screen
for (int i=0; i<5; i++) { echo $i }
```

**While Loop:**

```csharp
# Prints the variable i as long as it is less than 5, and increments it by one in each step
int i=0
while ($i < 5) { echo $i && i++ }
```

### 2. Batch (.bat) File Generation

One of `CmdCs`'s most powerful features is its ability to translate your C#-like code into an executable `.bat` file. This is done using the `public class` syntax. In this mode, structures like `for` and `while` are converted by `BatchTranslator` into pure Batch code containing `GOTO` and `IF` commands.

**Example:**
When you enter the following commands into the `CmdCs` console, a file named `DonguTest.bat` will be created.

1.  Enter the following command to start file writing mode:

    ```csharp
    public class LoopTest : bat {
    ```

2.  Then, write the code you want to be translated into the `.bat` file:

    ```csharp
    rem This is a for loop test
    for (int i=0; i<5; i++) {
        echo Counter: $i
    }
    echo Loop finished!
    ```

3.  Enter the `}` character to finish writing:
    ```csharp
    }
    ```

**Content of the Generated `LoopTest.bat` File:**

```batch
@echo off
rem This is a for loop test
rem For Loop Start
set /a i=0
:FOR_START_1
IF NOT %i% LSS 5 GOTO FOR_END_1
echo Counter: %i%
set /a i=%i%+1
GOTO FOR_START_1
:FOR_END_1
echo Loop finished!

GOTO :EOF
```

When you run this file, you will see the loop working even in a standard Windows environment without `CmdCs`.

## 📚 Command Reference

| Command                                            | Description                                                                                          |
| -------------------------------------------------- | ---------------------------------------------------------------------------------------------------- |
| `set a=10`                                         | Defines a variable. Mathematical expressions can also be used.                                       |
| `int a=10`                                         | Defines an integer type variable.                                                                    |
| `string a="hello"`                                 | Defines a string type variable.                                                                      |
| `echo $a`                                          | Prints the value of variable `a` to the screen.                                                      |
| `int[] nums={1,2,3}`                               | Defines an integer array.                                                                            |
| `string[] arr={"a","b"}`                           | Defines a string array.                                                                              |
| `for (int i=0; i<5; i++){...}`                     | A `for` loop that executes the code block as long as the specified condition is met.                 |
| `while ($i<5){...}`                                | A `while` loop that executes as long as the condition is true.                                       |
| `do {...} while ($i<5){...}`                       | A `do-while` loop that executes the block first, then checks the condition.                          |
| `foreach ($item in $arr[]){...}`                   | Executes the code block for each element in an array.                                                |
| `dir \| find "test" ? echo Found : echo Not Found` | If "test" is found in the output of the `dir` command, prints "Found", otherwise prints "Not Found". |
| `public class Name : extension { ... }`            | Creates a file with the specified name and extension and starts content writing mode. Ends with `}`. |
| `help2`, `help3`                                   | Displays help menus.                                                                                 |
| `cls`                                              | Clears the screen.                                                                                   |
| `hdd`                                              | Shows free space on drive C in GB.                                                                   |
