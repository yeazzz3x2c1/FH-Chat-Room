using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client_Application
{
    internal class Global
    {
        private static Server Server;
        private static TaskScheduler ui_scheduler;
        private static Window window;
        public static void Set_Server(Server Server)
        {
            if (Global.Server == null) Global.Server = Server;
        }
        public static Server Get_Server() => Server;
        public static void Open_Window(Window window)
        {
            Window old_window = Global.window;
            Global.window = window;
            window.Show();
            if (old_window != null)
                old_window.Close();
        }
        public static void Initialize_First_Window(Window window)
        {
            Global.window = window;
            Global.ui_scheduler = TaskScheduler.FromCurrentSynchronizationContext();
        }
        public static Window Get_Current_Window()
        {
            return Global.window;
        }
        public static void Run_Task_On_UI(Task task)
        {
            task.Start(ui_scheduler);
        }

        public static void Disconnect_From_Server()
        {
            Server.Disconnect();
        }
    }
}
