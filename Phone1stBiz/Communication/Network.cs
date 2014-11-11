//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Phone1stBiz.Communication
//{
//    class Network
//    {

//        private Socket sock;

//        private MainApp app;

//        private Thread NetworkThread;

//        public Network(MainApp app)
//        {

//            this.app = app;

//        }



//        public bool Connect()
//        {

//            bool isConnect = true;

//            if (sock != null) sock.Close();

//            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

//            while (true)
//            {

//                try
//                {

//                    IPAddress svrIp = IPAddress.Parse("127.0.0.1");

//                    IPEndPoint ipep = new IPEndPoint(svrIp, 9507);

//                    sock.Connect(ipep);

//                    if (sock.Connected)
//                    {

//                        isConnect = true;

//                        break;

//                    }

//                }

//                catch (SocketException ex)
//                {

//                    isConnect = false;

//                    app.ExceptionWrite("서버와 연결을 실패. 재접속 실행함.");

//                }

//            }

//            app.ExceptionWrite("서버와 연결 됨.");

//            return isConnect;

//        }



//        public void Receive()
//        {

//            while (true)
//            {

//                Thread.Sleep(500);

//                byte[] data = new byte[2048];

//                try
//                {

//                    long startTicks = DateTime.Now.Ticks;

//                    int size = sock.Receive(data);



//                    if (size <= 0) { Connect(); }

//                    else app.ConvertData(Encoding.Default.GetString(data, 0, size));



//                    TimeSpan workticks = new TimeSpan(DateTime.Now.Ticks - startTicks);

//                    app.ExceptionWrite("서버로부터 받은 데이터 : "

//                        + size + "Byte / 걸린시간 : " + workticks.TotalSeconds + "초");

//                }

//                catch (SocketException ex)
//                {

//                    app.ExceptionWrite(ex.Message);

//                    Connect();

//                }

//            }

//        }



//        public void ThreadStart()
//        {

//            NetworkThread = new Thread(new ThreadStart(Receive));

//            NetworkThread.Start();

//        }



//        public void ThreadAbort()
//        {

//            if (NetworkThread != null) NetworkThread.Abort();

//            if (sock.Connected) sock.Close();

//        }

//    }

//}
