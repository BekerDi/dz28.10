//Я создала 2 репозитория, потому что vs не загружал на гитхаб сразу 2 решения. 

using System;
using System.Collections.Generic;
using System.Linq;

public class Project
{
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public string Initiator { get; set; }
    public string ProjectLead { get; set; }
    public List<Task> Tasks { get; set; }
    public ProjectStatus Status { get; set; }

    public Project(string description, DateTime deadline, string initiator, string projectLead)
    {
        Description = description;
        Deadline = deadline;
        Initiator = initiator;
        ProjectLead = projectLead;
        Tasks = new List<Task>();
        Status = ProjectStatus.Open;
    }

    public void AddTask(Task task)
    {
        if (Status == ProjectStatus.Open)
        {
            Tasks.Add(task);
            task.Status = TaskStatus.Assigned;
        }
        else
        {
            Console.WriteLine("Невозможно добавить задачу в закрытый проект.");
        }
    }

    public void UpdateStatus()
    {
        bool allTasksCompleted = Tasks.All(t => t.Status == TaskStatus.Completed);
        if (allTasksCompleted)
        {
            Status = ProjectStatus.Closed;
        }
        else
        {
            Status = ProjectStatus.InProgress;
        }
    }
}

public enum ProjectStatus
{
    Open,
    InProgress,
    Closed
}

public class Task
{
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public string Initiator { get; set; }
    public string Assignee { get; set; }
    public TaskStatus Status { get; set; }
    public List<Report> Reports { get; set; }

    public Task(string description, DateTime deadline, string initiator, string assignee)
    {
        Description = description;
        Deadline = deadline;
        Initiator = initiator;
        Assignee = assignee;
        Status = TaskStatus.Assigned;
        Reports = new List<Report>();
    }

    public void AssignTask(string assignee)
    {
        Assignee = assignee;
        Status = TaskStatus.Assigned;
    }

    public void StartTask()
    {
        if (Status == TaskStatus.Assigned)
        {
            Status = TaskStatus.InProgress;
        }
        else
        {
            Console.WriteLine("Невозможно начать задачу. Задача не назначена.");
        }
    }

    public void CompleteTask()
    {
        if (Status == TaskStatus.InProgress)
        {
            Status = TaskStatus.Completed;
        }
        else
        {
            Console.WriteLine("Невозможно завершить задачу. Задача не в процессе выполнения.");
        }
    }

    public void AddReport(Report report)
    {
        Reports.Add(report);
    }
}

public enum TaskStatus
{
    Assigned,
    InProgress,
    Completed
}

public class Report
{
    public string Text { get; set; }
    public DateTime CompletionDate { get; set; }
    public string Performer { get; set; }

    public Report(string text, DateTime completionDate, string performer)
    {
        Text = text;
        CompletionDate = completionDate;
        Performer = performer;
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<string> teamMembers = new List<string> { "Алексей", "Мария", "Иван", "Елена", "Дмитрий", "Ольга", "Павел", "Анна", "Сергей", "Екатерина" };

        Project newProject = new Project("Новый проект", DateTime.Now.AddMonths(3), "Иванов", "Петров");

        for (int i = 0; i < 10; i++)
        {
            Task newTask = new Task($"Задача {i + 1}", DateTime.Now.AddMonths(1), newProject.Initiator, teamMembers[i % teamMembers.Count]);
            newProject.AddTask(newTask);
        }

        newProject.UpdateStatus();

        foreach (var task in newProject.Tasks)
        {
            Console.WriteLine($"Задача '{task.Description}': {GetStatus(task.Status)}");
        }

        Console.WriteLine($"Статус проекта: {GetStatus(newProject.Status)}");

        Console.WriteLine($"Пользователь: {newProject.ProjectLead}");

        Console.ReadLine();
    }

    public static string GetStatus(TaskStatus status)
    {
        switch (status)
        {
            case TaskStatus.Assigned:
                return "Назначена";
            case TaskStatus.InProgress:
                return "В процессе";
            case TaskStatus.Completed:
                return "Завершена";
            default:
                return "Неизвестный статус";
        }
    }

    public static string GetStatus(ProjectStatus status)
    {
        switch (status)
        {
            case ProjectStatus.Open:
                return "Открыт";
            case ProjectStatus.InProgress:
                return "В процессе";
            case ProjectStatus.Closed:
                return "Закрыт";
            default:
                return "Неизвестный статус";
        }
    }
}



