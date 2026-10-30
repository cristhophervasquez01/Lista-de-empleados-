using ProyectoTarea.Models;
using ProyectoTarea.Services;
using Spectre.Console;

class Program {
  public static bool running = true;
  public static string[] choices = [
    "1. Ver empleados",
    "2. Eliminar empleado",
    "3. Agregar empleado",
    "4. Salir"
  ];

  public static GestorService gestorService = new();

  public static void Main() {
    while (running) {
      var selections = new SelectionPrompt<string>()
        .Title("[PaleGreen1]Indica la acción a realizar:[/]")
        .HighlightStyle("DarkViolet_1")
        .AddChoices(choices);

      var option = AnsiConsole.Prompt(selections);
      List<Empleado> empleados = gestorService.FindAll();

      switch (option) {
        case "1. Ver empleados":
          AnsiConsole.Clear();

          var table = new Table();
          table.AddColumn("Id");
          table.AddColumn("Nombre");
          table.AddColumn("Cargo");
          table.AddColumn("Departamento");
          table.AddColumn("Horario");

          foreach (Empleado emp in empleados) {
            table.AddRow(
              emp.Id.ToString(),
              emp.Nombre,
              emp.Cargo,
              emp.Departamento,
              emp.Horario
            );
          }

          AnsiConsole.Write(table);
          break;

        case "2. Eliminar empleado":
          AnsiConsole.Clear();
          int id = AnsiConsole.Ask<int>("Indica el id que quieres eliminar: ");

          AnsiConsole.WriteLine("");
          var confirmed = AnsiConsole.Confirm("¿Estás seguro?");

          if (confirmed) {
            bool deleted = gestorService.Delete(id);

            if (deleted) {
              AnsiConsole.MarkupLine("\n[DarkViolet_1]Empleado eliminado![/]\n");
              break;
            }

            AnsiConsole.MarkupLine("\n[red]Empleado no encontrado![/]\n");
          }
          else {
            AnsiConsole.MarkupLine("\n[yellow]Operación cancelada![/]\n");
          }
          break;

        case "3. Agregar empleado":
          AnsiConsole.Clear();
          string nombre = AnsiConsole.Ask<string>("[DarkGoldenrod]Nombre del empleado: [/]");
          string cargo = AnsiConsole.Ask<string>("[DarkGoldenrod]Cargo: [/]");
          string departamento = AnsiConsole.Ask<string>("[DarkGoldenrod]Departamento: [/]");
          string horario = AnsiConsole.Ask<string>("[DarkGoldenrod]Horario: [/]");

          var maxId = empleados.Any() ? empleados.Max((b) => b.Id) : 0;

          Empleado newEmpleado = new() {
            Id = maxId + 1,
            Nombre = nombre,
            Cargo = cargo,
            Departamento = departamento,
            Horario = horario
          };

          gestorService.Create(newEmpleado);
          AnsiConsole.MarkupLine("\n[DarkViolet_1]Empleado agregado![/]\n");
          break;

        case "4. Salir":
          AnsiConsole.Clear();
          AnsiConsole.MarkupLine("[DarkViolet_1]¡Fin del programa![/]");
          running = false;
          break;
      }
    }
  }
}