using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int INF = int.MaxValue;

        public MainWindow()
        {
            InitializeComponent();
        }



        private void DijkstraButton_Click(object sender, RoutedEventArgs e)
        {
            int n = int.Parse(VerticesTextBox.Text);


            List<List<int>> graph = new List<List<int>>();
            for (int i = 0; i < n; i++)
            {
                graph.Add(new List<int>());
                for (int j = 0; j < n; j++)
                {
                    graph[i].Add(0);
                }
            }


            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int weight;
                    if (i != j)
                    {
                        string input = Interaction.InputBox($"Между вершиной {i} и {j} (0, если нет связи):", "Вес ребра");
                        if (!int.TryParse(input, out weight))
                        {
                            MessageBox.Show("Введите корректное число.");
                            return;
                        }
                        graph[i][j] = weight;
                    }
                }
            }

            int startVertex;
            if (!int.TryParse(StartVertexTextBox.Text, out startVertex) || startVertex < 0 || startVertex >= n)
            {
                MessageBox.Show("Введите корректное значение начальной вершины.");
                return;
            }

            Dijkstra(graph, startVertex);
        }


        public void Dijkstra(List<List<int>> graph, int start)
            {
                int n = graph.Count;
                int[] distance = new int[n];
                bool[] visited = new bool[n];

                for (int i = 0; i < n; i++)
                {
                    distance[i] = INF;
                    visited[i] = false;
                }

                distance[start] = 0;

                for (int i = 0; i < n - 1; i++)
                {
                    int minDist = INF, minIndex = -1;

                    for (int j = 0; j < n; j++)
                    {
                        if (!visited[j] && distance[j] < minDist)
                        {
                            minDist = distance[j];
                            minIndex = j;
                        }
                    }

                    visited[minIndex] = true;

                    for (int j = 0; j < n; j++)
                    {
                        if (!visited[j] && graph[minIndex][j] != 0 && distance[minIndex] != INF &&
                            distance[minIndex] + graph[minIndex][j] < distance[j])
                        {
                            distance[j] = distance[minIndex] + graph[minIndex][j];
                        }
                    }
                }


                    TextBlock resultTextBlock = new TextBlock
                    {
                        Text = $"До вершины {n - 1}: {distance[n - 1]}", 
                        Foreground = Brushes.Black
                    };
                    ResultsStackPanel.Children.Add(resultTextBlock);
               
            }
        }
    }

