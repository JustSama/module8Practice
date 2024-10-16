using System;
using System.Collections.Generic;

public interface IMediator
{
    void SendMessage(string message, IUser user, string channel);
    void AddUser(IUser user, string channel);
    void RemoveUser(IUser user, string channel);
}

public class ChatMediator : IMediator
{
    private readonly Dictionary<string, List<IUser>> channels = new();

    public void SendMessage(string message, IUser user, string channel)
    {
        if (!channels.ContainsKey(channel))
        {
            Console.WriteLine($"Канал {channel} не существует. Создаю новый.");
            channels[channel] = new List<IUser>();
        }

        if (!channels[channel].Contains(user))
        {
            Console.WriteLine($"{user.GetName()} не состоит в канале {channel}. Сообщение не отправлено.");
            return;
        }

        foreach (var participant in channels[channel])
        {
            if (participant != user)
            {
                participant.ReceiveMessage(message, channel);
            }
        }
    }

    public void AddUser(IUser user, string channel)
    {
        if (!channels.ContainsKey(channel))
        {
            channels[channel] = new List<IUser>();
            Console.WriteLine($"Создан новый канал: {channel}");
        }

        channels[channel].Add(user);
        Console.WriteLine($"{user.GetName()} присоединился к каналу {channel}.");
    }

    public void RemoveUser(IUser user, string channel)
    {
        if (channels.ContainsKey(channel) && channels[channel].Contains(user))
        {
            channels[channel].Remove(user);
            Console.WriteLine($"{user.GetName()} покинул канал {channel}.");
        }
    }
}

public interface IUser
{
    void SendMessage(string message, string channel);
    void ReceiveMessage(string message, string channel);
    string GetName();
}

public class User : IUser
{
    private readonly IMediator mediator;
    private readonly string name;

    public User(IMediator mediator, string name)
    {
        this.mediator = mediator;
        this.name = name;
    }

    public void SendMessage(string message, string channel)
    {
        mediator.SendMessage(message, this, channel);
    }

    public void ReceiveMessage(string message, string channel)
    {
        Console.WriteLine($"{GetName()} получил сообщение в канале {channel}: {message}");
    }

    public string GetName()
    {
        return name;
    }
}

class Program
{
    static void Main(string[] args)
    {
        ChatMediator mediator = new ChatMediator();

        User user1 = new User(mediator, "Alice");
        User user2 = new User(mediator, "Bob");
        User user3 = new User(mediator, "Charlie");

        mediator.AddUser(user1, "General");
        mediator.AddUser(user2, "General");
        mediator.AddUser(user3, "General");

        user1.SendMessage("Привет, команда!", "General");
        user2.SendMessage("Привет, Alice!", "General");

        mediator.RemoveUser(user2, "General");
        user1.SendMessage("Кто-нибудь здесь?", "General");

        mediator.AddUser(user2, "General");
        user2.SendMessage("Извините, я был вне сети.", "General");
    }
}
