using System;
using System.Collections.Generic;

public interface ICommand
{
    void Execute();
    void Undo();
}

public class Light
{
    public void On() => Console.WriteLine("Свет включен");
    public void Off() => Console.WriteLine("Свет выключен");
}

public class LightOnCommand : ICommand
{
    private Light light;

    public LightOnCommand(Light light)
    {
        this.light = light;
    }

    public void Execute() => light.On();
    public void Undo() => light.Off();
}

public class LightOffCommand : ICommand
{
    private Light light;

    public LightOffCommand(Light light)
    {
        this.light = light;
    }

    public void Execute() => light.Off();
    public void Undo() => light.On();
}

public class RemoteControl
{
    private ICommand lastCommand;

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        lastCommand = command;
    }

    public void UndoLastCommand()
    {
        if (lastCommand != null)
        {
            lastCommand.Undo();
        }
        else
        {
            Console.WriteLine("Нет команды для отмены");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        RemoteControl remote = new RemoteControl();
        Light light = new Light();

        ICommand lightOn = new LightOnCommand(light);
        ICommand lightOff = new LightOffCommand(light);

        remote.ExecuteCommand(lightOn);
        remote.ExecuteCommand(lightOff);
        remote.UndoLastCommand();
        remote.UndoLastCommand();
    }
}
