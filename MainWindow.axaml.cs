using System;
using System.IO.Ports;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;

namespace ProgramablesAvalonia
{
    public partial class MainWindow : Window
    {
        SerialPort serialPort = new SerialPort();
        Datos datos = new Datos();

        public MainWindow()
        {
            InitializeComponent();

            serialPort.DataReceived += DataReceive;

            // cargar puertos disponibles
            foreach (var port in SerialPort.GetPortNames())
            {
                cmbPuertos.Items.Add(port);
            }
        }

        private void DataReceive(object? sender, SerialDataReceivedEventArgs e)
        {
            string data = serialPort.ReadLine();
            datos.ejecutar($"INSERT INTO temperaturas (temperatura,fecha,hora) VALUES ({data},'{DateTime.Now:yyyy-MM-dd}','{DateTime.Now:HH:mm:ss}')");            
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                rtbMenssage.Text += data + Environment.NewLine;
            });
        }

        private async void btnConectar_Click(object? sender, RoutedEventArgs e)
        {
            try
            {
                serialPort.PortName = cmbPuertos.SelectedItem?.ToString();
                serialPort.BaudRate = 9600;
                serialPort.Open();

                await MostrarMensaje("Conectado");
            }
            catch (Exception ex)
            {
                await MostrarMensaje("Error: " + ex.Message);
            }
        }

        private async System.Threading.Tasks.Task MostrarMensaje(string mensaje)
        {
            var ventana = new Window
            {
                Width = 300,
                Height = 150,
                Title = "Sistema",
                Content = new TextBlock
                {
                    Text = mensaje,
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                    VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
                }
            };

            await ventana.ShowDialog(this);
        }
    }
}