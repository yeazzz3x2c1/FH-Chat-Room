using Server_Application;

Console.WriteLine("聊天室伺服器 Ver 1.0");
Server server = new Server();
server.Build_Up();
while (true)
{
    bool shutdown = false;
    string commend = Console.ReadLine();
    switch (commend)
    {
        case "shutdown":
            shutdown = true;
            break;
        default:
            Console.WriteLine("未知的指令: " + commend);
            break;
    }
    if (shutdown)
        break;
}
server.ShutDown();
Console.WriteLine("伺服器已關閉");