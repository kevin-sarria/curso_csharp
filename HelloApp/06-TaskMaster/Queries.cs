using System.Drawing;
using BetterConsoleTables;

namespace TaskMaster
{
  public class Queries(List<Task> _tasks)
  {
    private List<Task> Tasks = _tasks;

    public void ListTasks()
    {
      // ForegroundColor = ConsoleColor.DarkBlue;
      WriteLine("-----Lista de tareas-----");

      Table table = new Table("Id", "Descripcion", "Estado");

      foreach (var task in Tasks)
      {
        table.AddRow(task.Id, task.Description, task.Completed ? "Completada" : "Vacio").AddRow();
      }

      table.Config = TableConfiguration.Unicode();

      Write(table.ToString());
      ReadKey();
    }

    public List<Task> AddTask()
    {
      try
      {
        ResetColor();
        Clear();
        WriteLine("---Agregar tarea---");
        WriteLine("Ingrese la descripcion de la tarea: ");
        var description = ReadLine()!;
        Task newTask = new Task(Utils.GenerateId(), description);
        Tasks.Add(newTask);
        ForegroundColor = ConsoleColor.Green;
        WriteLine("Tarea agregada con exito");
        ResetColor();
        return Tasks;
      }
      catch (Exception ex)
      {
        ForegroundColor = ConsoleColor.Red;
        WriteLine(ex.Message);
        return Tasks;
      }
    }

    public List<Task> MarkAsCompleted()
    {
      try
      {
        ResetColor();
        Clear();
        WriteLine("---Marcar tarea como completada---");
        WriteLine("Ingrese el id de la tarea que desea marcar como completada: ");
        var id = ReadLine()!;
        var task = Tasks.Find(t => t.Id == id)!;
        if (task == null)
        {
          ForegroundColor = ConsoleColor.Red;
          WriteLine("No se encontro la tarea con el id proporcionado");
          ResetColor();
          return Tasks;
        }

        task.Completed = true;
        task.ModifiedAt = DateTime.Now;
        ForegroundColor = ConsoleColor.Green;
        WriteLine("Tarea marcada como completada con exito");
        ResetColor();
        return Tasks;
      }
      catch (Exception ex)
      {
        ForegroundColor = ConsoleColor.Red;
        WriteLine(ex.Message);
        return Tasks;
      }
    }

    public List<Task> EditTask()
    {
      try
      {
        ResetColor();
        Clear();
        WriteLine("---Editar tarea---");
        WriteLine("Ingrese el id para editar la tarea: ");
        var id = ReadLine()!;
        var task = Tasks.Find(t => t.Id == id)!;
        if (task == null)
        {
          ForegroundColor = ConsoleColor.Red;
          WriteLine("No se encontro la tarea con el id proporcionado");
          ResetColor();
          return Tasks;
        }

        Write("Ingrese la descripcion de la tarea: ");
        var description = ReadLine()!;
        task.Description = description;
        task.ModifiedAt = DateTime.Now;
        ForegroundColor = ConsoleColor.Green;
        WriteLine("Tarea modificada con exito");
        ResetColor();
        return Tasks;
      }
      catch (Exception ex)
      {
        ForegroundColor = ConsoleColor.Red;
        WriteLine(ex.Message);
        return Tasks;
      }
    }

    public List<Task> RemoveTask()
    {
      try
      {
        ResetColor();
        Clear();
        WriteLine("---Eliminar tarea---");
        WriteLine("Ingrese el id para eliminar la tarea: ");
        var id = ReadLine()!;
        var task = Tasks.Find(t => t.Id == id)!;
        if (task == null)
        {
          ForegroundColor = ConsoleColor.Red;
          WriteLine("No se encontro la tarea con el id proporcionado");
          ResetColor();
          return Tasks;
        }

        Tasks.Remove(task);
        ForegroundColor = ConsoleColor.Green;
        WriteLine("Tarea eliminada con exito");
        ResetColor();
        return Tasks;
      }
      catch (Exception ex)
      {
        ForegroundColor = ConsoleColor.Red;
        WriteLine(ex.Message);
        return Tasks;
      }
    }

    public void TasksByState()
    {
      Clear();
      try
      {
        ResetColor();
        WriteLine("---Tareas por estado---");
        WriteLine("1. Completadaas");
        WriteLine("2. Pendientes");
        Write("Ingrese la opcion de las tareas a mostrar: ");
        string taskState = ReadLine()!;
        if (taskState != "1" && taskState != "2")
        {
          ForegroundColor = ConsoleColor.Red;
          WriteLine("Opcion no valida");
          ResetColor();
          return;
        }
        bool completed = taskState == "1";
        List<Task> filteredTasks = Tasks.Where(t => t.Completed == completed).ToList();
        if (filteredTasks.Count == 0)
        {
          ForegroundColor = ConsoleColor.Red;
          WriteLine("No se encontraron tareas con el estado solicitado");
          ResetColor();
          return;
        }

        ForegroundColor = completed ? ConsoleColor.Green : ConsoleColor.Red;
        Table table = new Table("Id", "Descripcion", "Estado");

        foreach (var task in filteredTasks)
        {
          table.AddRow(task.Id, task.Description, task.Completed ? "Completada" : "Vacio").AddRow();
        }

        table.Config = TableConfiguration.Unicode();

        Write(table.ToString());
        ReadKey();
      }
      catch (Exception ex)
      {
        ForegroundColor = ConsoleColor.Red;
        WriteLine($"Ocurrio un error al filtrar las tareas: {ex.Message}");
      }
    }

    public void TasksByDescription()
    {
      Clear();
      try
      {
        ResetColor();
        WriteLine("---Tareas por descripcion---");
        Write("Ingrese la descripcion de las tareas a buscar: ");
        string description = ReadLine()!;
        List<Task> matchingTasks = Tasks.FindAll(t => t?.Description.Contains(description, StringComparison.OrdinalIgnoreCase) ?? false).ToList();
        if (matchingTasks.Count == 0)
        {
          ForegroundColor = ConsoleColor.Red;
          WriteLine("No se encontraron tareas con la descripcion proporcionada");
          ResetColor();
          return;
        }

        Table table = new Table("Id", "Descripcion", "Estado");

        foreach (var task in matchingTasks)
        {
          table.AddRow(task.Id, task.Description, task.Completed ? "Completada" : "Vacio").AddRow();
        }

        table.Config = TableConfiguration.Unicode();

        Write(table.ToString());
        ReadKey();
      }
      catch (Exception ex)
      {
        ForegroundColor = ConsoleColor.Red;
        WriteLine($"Ocurrio un error al filtrar las tareas por descripcion: {ex.Message}");
      }
    }

  }
}