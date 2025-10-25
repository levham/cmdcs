# CmdCs - Command Line Interpreter (v.1.12.x)

[_Turkish.v.1.12_](document_cmdcs_v.1.12.x.tr.md)

`CmdCs` is an advanced command-line tool that enhances the capabilities of the standard Windows command line (CMD) with C#-like syntax and can translate these commands into executable Batch (`.bat`) files. This project can run both as an interactive shell and directly convert code blocks containing C#-like loop and variable logic into `.bat` scripts.

## ✨ Key Features

- **C#-Like Syntax:** Provides a C#-like structure for `for`, `while`, `do-while` loops, `int`, `string` variable definitions, and mathematical operations.
- **Batch File Generation:** Automatically converts C#-like code you write using the `public class <FileName> : bat { ... }` structure into a valid `.bat` file.
- **Variable Management:** Ability to use variables with the `$variable` format, define them with `set`, `int`, `string`, and perform mathematical operations.
- **Array Support:** Define arrays in `int[]` and `string[]` formats, and traverse these arrays with the `foreach` loop.
- **Conditional Commands:** Support for the `ternary` operator, which executes different commands based on the output of CMD commands (`condition ? if true : if false`).
- **Configuration File:** Manage startup settings (colors, notes, etc.) via the `Settings.cs` file.
- **Extensible Command Set:** Internal helper commands such as `help`, `hdd`.

## Configuration (Setting.txt)

You can customize the application's behavior by editing the `Setting.txt` file.

| Setting       | Sample Value      | Description                                                                                                                                      |
| ------------- | ----------------- | ------------------------------------------------------------------------------------------------------------------------------------------------ |
| `linecolor`   | `Yellow,White`    | Sets the two alternative colors of the prompt (`>>`).                                                                                            |
| `defaultpage` | `Display1`        | Changes the startup message. `Display1` displays the English welcome message, while any other value (e.g., `Display2`) displays the Turkish one. |
| `note`        | `true` or `false` | When set to `true`, displays additional notes at startup. When set to `false`, hides them.                                                       |

## 🚀 Usage

## General

| Command          | Description                                   |
| ---------------- | --------------------------------------------- |
| `help2`, `help3` | Displays the help menus.                      |
| `hdd`            | Displays the free space on the C drive in GB. |
| `cls`            | Clears the screen.                            |
| # comment        | Comment line.                                 |
| // comment       | Comment line.                                 |

## Variable

| ------------------------------------------ | ------------------------------------------------------------------------------------------------ |
| `set a=10` | Defines a variable. Mathematical expressions can also be used. |
| `int a=10` | Defines an integer variable. |
| `string a="hello"` | Defines a string variable. |
| `echo $a` | Prints the value of variable `a`. |

```bash
# Define a numeric variable
int i = 10

# Print the value of the variable
echo $i

# Define a new variable with a mathematical operation
set j = $i * 2

# Display the value of j
echo $j
```

## Array

| ------------------------------------------ | ------------------------------------------------------------------------------------------------ |
| `int[] nums={1,2,3}` | Defines an integer array. |
| `string[] arr={"a","b"}` | Defines a text array. |

```bash
# Define a numeric variable
int[] nums={1,2,3}

# Define a text variable
string[] arr={"a","b"}

# Print the value of the variable
echo $nums

# Print the value of the variable
echo $arr

```

## Loops

| ------------------------------------------ | ------------------------------------------------------------------------------------------------ |
| `for (int i=0; i<5; i++){...}` | A `for` loop that executes a block of code as long as a specified condition is true. |
| `while ($i<5){...}` | A `while` loop that executes as long as a condition is true. |
| `do {...} while ($i<5){...}` | A `do-while` loop that executes the block first and then checks the condition. |
| `foreach ($item in $arr[]){...}` | Executes a block of code for each element in an array. |

**For Loop:**

```csharp
# Prints numbers from 0 to 4
for (int i=0; i<5; i++) { echo $i }
```

**While Loop:**

```csharp
# Prints the variable i as long as it is less than 5 and increments it by one at each step.
int i=0
while ($i < 5) { echo $i && i++ }
```

**Do While Loop:**

```csharp
# The do block runs once, then the loop continues as long as the while condition is true.
do { set i=0 } while ( $i < 5 ) { echo $i && i++ }
```

**Foreach Loop:**

```csharp
# traverses array a with foreach, printing each element to the screen.
int[] a={1,2,3}
foreach ($b in $a[]) { echo $b }

```

## Ternary

| ------------------------------------------ | ------------------------------------------------------------------------------------------------ |
| `dir \| find "test" ? echo Yes : echo None` | If "test" is present in the output of the `dir` command, it prints "Yes", otherwise it prints "None". |

```bash
# checks the output of the dir command.
dir | find "z" ? echo Yes : echo No
```

## File printer

| ------------------------------------------ | ------------------------------------------------------------------------------------------------ |
| `public class Name: extension { ... }` | Creates a file with the specified name and extension and starts content writing mode. It ends with `}`. |

One of the features of `CmdCs` that needs improvement is the ability to convert the C#-like code you write into an executable `.bat` file. This is done with the `public class` syntax.

**Example:**
When you enter the following commands into the `CmdCs` console, a file named `DonguTest.bat` will be created.

1. Enter the following command to start file writing mode:

```csharp
public class DonguTest : bat {
```

2. Next, enter the code you want to translate into the `.bat` file:

```csharp
rem This is a for loop test
rem any bat commands
echo Loop finished!
```

3. Enter the `}` character to finish writing:

```csharp
}
```

**Contents of the generated `DonguTest.bat` file:**

```batch
@echo off
rem This is a for loop test
rem any bat commands
echo Loop finished!
```
