using CapaNegocios;
using Clases;
using log4net;
using Rbt.Business;
using System;
using System.Security.Permissions;
using System.ServiceProcess;
using System.Threading;
using System.Timers;

namespace ServRbtCodeudorMicre
{
    [UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
    public partial class ServRbtCodeudorMicre : ServiceBase
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ServRbtCodeudorMicre));
        private System.Timers.Timer myTimer = new System.Timers.Timer(); ///***
        Logs log;// = new Logs();

        public ServRbtCodeudorMicre()
        {
            log4net.Config.XmlConfigurator.Configure();
            try
            {
                InitializeComponent();
                log = new Logs();
                myTimer = new System.Timers.Timer(Convert.ToInt32(FuncionesComunes.GetValueAppSettingByKey("INTERVALO_EN_MINUTOS")) * 1000 * 60);// ConfigurationManager.AppSettings["INTERVALO_EN_MINUTOS"].ToString()) * 1000 * 60 );
                myTimer.AutoReset = true;
                myTimer.Elapsed += new ElapsedEventHandler(myTimer_Elapsed);
            }
            catch (Exception ex)
            {
                Logger.Error("Error en la instanciación del SevrRbtMicredito" + ex.ToString());
            }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                log.registroTXT(" #$%*************** TURNNING ON THE ROBOT (OnStart) ************#$% ");
                Logger.Info("Iniciando el Robot (On start)");
                myTimer.Start();
            }
            catch (Exception ex)
            {
                Logger.Error("Robot - OnStart, Ocurrio un error tratando de iniciar el Robot.", ex);
            }
        }

        protected override void OnStop()
        {
            try
            {
                myTimer.Stop();
                myTimer.Dispose();
                Logger.Info("Robot - OnStop, Turnning off the Robot, launching the OnStop method");
                log.registroTXT(" \n #$%*************** SHUTTING DOWN THE ROBOT (OnStop) ************#$% \n");
            }
            catch (Exception ex)
            {
                Logger.Error("Robot - OnStop, Ocuirrio un error tratando de parar el Robot.", ex);
            }
        }

        public void myTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (this.myTimer)
            {
                ProcesoExtra proceso = new ProcesoExtra();
                proceso.CrearCodeudor();
            }
        }

        private void eventLog1_EntryWritten(object sender, System.Diagnostics.EntryWrittenEventArgs e)
        {

        }
    }
}
