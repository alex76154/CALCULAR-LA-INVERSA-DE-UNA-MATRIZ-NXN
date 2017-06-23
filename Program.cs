using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matriz_inversa
{
    class Program
    {
        static void Cargar_matriz(double[,] matriz, int d)
        {
            for (int i = 0; i < d; i++)
            {
                for (int j = 0; j < d; j++)
                {
                    Console.Write("Matriz["+i+","+j+"]:");
                    int a = int.Parse(Console.ReadLine());
                    matriz[i, j] = a;
                }
            }
        }
        static void Mostrar_matriz(double[,] matriz, int d)
        {
            for (int i = 0; i < d; i++)
            {
                for (int j = 0; j < d; j++)
                {
                    Console.Write(matriz[i,j]);
                    Console.Write("   ");

                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }
       /* static void Determinante(int[,] matriz, int dimension)
        {
            int d=0;
            int []vec=new int[dimension];
            /*if(dimension>1)
            {
                if (dimension == 2)
                {
                    d = (matriz[0, 0] * matriz[1, 1]) - (matriz[1, 0] * matriz[0, 1]);
                    Console.WriteLine("El determinante es:" + d);
                }
                else
                {
                    if (dimension == 3)
                    {
                        int suma = 0;
                        vec[0] = matriz[0, 0] * ((matriz[1, 1] * matriz[2, 2]) - (matriz[2, 1] * matriz[1, 2]));
                        vec[1] = -matriz[0, 1]* ((matriz[1, 0] * matriz[2, 2]) - (matriz[2, 0] * matriz[1, 2]));
                        vec[2] = matriz[0, 2] * ((matriz[1, 0] * matriz[2, 1]) - (matriz[2, 0] * matriz[1, 1]));
                        suma = vec[0] + vec[1] + vec[2];
                        Console.WriteLine("El determinante es:" + suma);
                    }
                }
            }
            else
            {
                Console.WriteLine("Error no matriz cuadrada");
            }
            
        
        }*/
        public static double det(double [,]max , int dim)
        {
            double d;
            if (dim==2)
            {
                d=(max[0,0]*max[1,1])-(max[1,0]*max[0,1]);
                return d;
            }
            else
            {
                double suma=0.0 ;
                for ( int i = 0; i < dim; i++)
                {
                    double[,] vec = new double[dim , dim ];
                    for (int j = 0; j < dim; j++)
                    {
                        if (j!=i)
                        {
                            for (int k = 1; k < dim; k++)
                            {
                                int indice = -1;
                                if (j < i)
                                    indice = j;
                                else if (j > i)
                                    indice = j - 1;
                                vec[indice,k-1]= max[j,k];
                            }
                        }
                    }
                    if (i % 2 == 0)
                        suma += max[i, 0] * det(vec ,dim-1);
                    else
                        suma -= max[i, 0] * det(vec,dim-1);
                }
                return suma;
            }
        }
        public static double[,] Matriz_Adjunta(double[,] matriz, int dim)
        {
            return matriz_cofactores(matriz,dim);
        }
        public static double[,] matriz_cofactores(double[,] matriz, int dim)
        {
            double[,] vec = new double[dim, dim];
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    double[,] de = new double[dim, dim];
                    double detvalor;
                    for (int k = 0; k < dim; k++)
                    {
                        if (k!=i)
                        {
                            for (int l = 0; l <dim; l++)
                            {
                                if (l!=j)
                                {
                                    int indice_1;
                                    int indice_2;
                                    if (k < i)
                                        indice_1 = k;
                                    else
                                        indice_1 = k - 1;
                                    if (l < j)
                                        indice_2 = l;
                                    else
                                        indice_2 = l - 1;
                                    
                                    de[indice_1, indice_2] = matriz[k, l];
                                }
                            }
                        }
                    }
                    detvalor = det(de, dim-1);
                    vec[i, j] = detvalor * (int)Math.Pow(-1,i+j+2);
                }
            }
            return vec;
        }
        public static double[,] transpuesta(double[,] matriz, int dim)
        {
            double[,] vec = new double[dim, dim];
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    vec[i, j] = matriz[j, i];
                }
            }
            return vec;
        }
        public static double[,] Matriz_inversa(double[,] trans, int dim, double determi)
        {
            double dt = determi;
            double[,] vec = trans;
            double[,] vec1 = new double[dim, dim]; 
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    vec1[i,j]=(vec[i, j] *1)/ dt;
                }
            }
           
            return vec1;
        }
        static void Main(string[] args)
        {
            
            Console.Write("Ingrese Dimension de Fila y Columna:");
            int Dim = int.Parse(Console.ReadLine());
            
            double[,] matriz= new double[Dim, Dim];
            double[,] max   = new double[Dim, Dim];
            double[,] trans = new double[Dim, Dim];
            double[,] Inver = new double[Dim, Dim];
            Cargar_matriz(matriz, Dim);
            Console.Clear();
            Mostrar_matriz(matriz, Dim);
            double suma = 0.0;
            suma=det(matriz, Dim);
            Console.WriteLine("El determinante  es :"+suma);
            if (suma!=0.0)
            {
                if (Dim==2)
                {
                    double d;
                    double[,] vec = new double[Dim, Dim];
                    d = 1 / suma;
                    vec[0, 0] = d*matriz[1, 1];
                    vec[1, 1] = d*matriz[0, 0];
                    vec[0, 1] = d*matriz[0, 1]*(-1);
                    vec[1, 0] = d*matriz[1, 0]*(-1);
                    Console.WriteLine("Matriz Inversa");
                    Mostrar_matriz(vec, Dim);
                    
                }
                else
                {
                    max = Matriz_Adjunta(matriz, Dim);
                    Console.WriteLine("Matriz Adjunta");
                    Mostrar_matriz(max, Dim);
                    trans = transpuesta(max, Dim);
                    Console.WriteLine("Matriz Transpuesta");
                    Mostrar_matriz(trans, Dim);

                    Console.WriteLine();
                    Console.WriteLine("Matriz Inversa");

                    Inver = Matriz_inversa(trans, Dim, suma);
                    Mostrar_matriz(Inver, Dim);
                }
                   
            }
            else
            {
                Console.WriteLine("No es una matriz Invertible");
            }
            
            Console.ReadKey();
        }
    }
}
