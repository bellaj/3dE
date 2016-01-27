using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using System.ComponentModel;
//using System.Drawing;
using _3DTools;
using System.Data.SqlClient;


namespace freelqnce
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        Trackball tr;
         int frame;

        SqlConnection myConnection;
        public MainWindow()
        {
            InitializeComponent();

 
            BuildSolid();
            labelrouge.Foreground = Brushes.Red;
            labelbleu.Foreground = Brushes.Blue;


//////////////////////////////get points

              myConnection = new SqlConnection("Server=KHALIDBELLAJ-PC;Database=ear;Trusted_Connection=True");


              String[,] cor;
              frame = 350;
              cor = get_points_from_db(frame, myConnection);
 

             int i = 0 ; 
                while (cor[i, 0] != null)
                {
                     
                   retrive_point(cor[i,0], cor[i,1], cor[i,2], cor[i,3], cor[i,4]);
                   i++;
              }
       
          



        }


        private GeometryModel3D mGeometry;
        private void create_3dpoint(double x, double y, double z)
        {
            SolidColorBrush col;
         
               col = System.Windows.Media.Brushes.AliceBlue;
 
  if (checkcolor1.IsChecked==true)
{
             
                checkcolor2.IsChecked = false;
                col = System.Windows.Media.Brushes.Red;
}
  else if (checkcolor2.IsChecked == true)
{
                 checkcolor1.IsChecked = false;
                col = System.Windows.Media.Brushes.Blue;

}
            mGeometry = new GeometryModel3D(Tessellate(x, y, z), new DiffuseMaterial(col));
            mGeometry.Transform = new Transform3DGroup();
            group.Children.Add(mGeometry);

             ////////////////insert into Database///////////////////////

            save_points_in_db(x, y, z, frame, col.ToString(), myConnection);

        
        }

        /// <returns></returns>
        internal static MeshGeometry3D Tessellate(double x,double y,double z)
        {
           int tDiv = 15;
           int pDiv = 25;
           double radius = 1.5;
            double dt = DegToRad(360.0) / tDiv;
            double dp = DegToRad(180.0) / pDiv;

            MeshGeometry3D meshh = new MeshGeometry3D();

            for (int pi = 0; pi <= pDiv; pi++)
            {
                double phi = pi * dp;

                for (int ti = 0; ti <= tDiv; ti++)
                {
                    // we want to start the mesh on the x axis
                    double theta = ti * dt;

                    meshh.Positions.Add(GetPosition(theta, phi, radius,x,y,z));
                    meshh.Normals.Add(GetNormal(theta, phi));
                  meshh.TextureCoordinates.Add(GetTextureCoordinate(theta, phi));
                }
            }

            for (int pi = 0; pi < pDiv; pi++)
            {
                for (int ti = 0; ti < tDiv; ti++)
                {
                    int x0 = ti;
                    int x1 = (ti + 1);
                    int y0 = pi * (tDiv + 1);
                    int y1 = (pi + 1) * (tDiv + 1);

                    meshh.TriangleIndices.Add(x0 + y0);
                    meshh.TriangleIndices.Add(x0 + y1);
                    meshh.TriangleIndices.Add(x1 + y0);

                    meshh.TriangleIndices.Add(x1 + y0);
                    meshh.TriangleIndices.Add(x0 + y1);
                    meshh.TriangleIndices.Add(x1 + y1);
                }
            }

            meshh.Freeze();
             return meshh;
        }
        private static Point3D GetPosition(double theta, double phi, double radius, double X, double Y, double Z)
        {
            double x = radius * Math.Sin(theta) * Math.Sin(phi)+X;
            double y = radius * Math.Cos(phi)+Y;
            double z = radius * Math.Cos(theta) * Math.Sin(phi)+Z;

            return new Point3D(x, y, z);
        }

        private static Vector3D GetNormal(double theta, double phi)
        {
            return (Vector3D)GetPosition(theta, phi, 1.0,0,0,0);
        }

        private static double DegToRad(double degrees)
        {
            return (degrees / 180.0) * Math.PI;
        }
        private static Point GetTextureCoordinate(double theta, double phi)
        {
            Point p = new Point(theta / (2 * Math.PI),
                                phi / (Math.PI));

            return p;
        }
   /// <summary>
   /// //////////////////////////////////////////
   /// </summary>

 
        private void BuildSolid()
        {

            tr = new Trackball();
            tr.EventSource = this.viewport;// src ev used in track.cs
            this.viewport.Camera.Transform = tr.Transform;

 

        }


/*
        public class Freel : INotifyPropertyChanged
        {
            private double vvertic = 0;
            private double hhoriz = 0;
            private double zaxe = 0;

            private Point3D axxe;


            public Point3D Axxe
            {
                get { return axxe; }
                set
                {
                    axxe = value; OnPropertyChanged("Axxe");
                }

            }

            public double Vertic
            {
                get { return vvertic; }
                set
                {
                    vvertic = value; OnPropertyChanged("Vertic");
                }

            }

            public double Horiz
            {
                get { return hhoriz; }
                set
                {
                    hhoriz = value; OnPropertyChanged("Horiz");
                }

            }

            public double Zaxe
            {
                get { return zaxe; }
                set
                {
                    zaxe = value; OnPropertyChanged("Zaxe");
                }

            }

            public event PropertyChangedEventHandler PropertyChanged;
            private void OnPropertyChanged(string propretyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propretyName));
                }
            }

        }

   */

        private void move_left(object sender, RoutedEventArgs e)
        {

            tr.left_right(sender, e, 0);
        }

        private void move_right(object sender, RoutedEventArgs e)
        {
             tr.left_right(sender, e, 1);
        }

        private void move_up(object sender, RoutedEventArgs e)
        {
            tr.clic(sender, e, 1);


        }

        public void move_down(object sender, RoutedEventArgs e)
        {

            tr.clic(sender, e, 0);



        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            tr.btn_zoom(sender, e, 0);
        }


        private void Button_Zoom_plus(object sender, RoutedEventArgs e)
        {
            tr.btn_zoom(sender, e, 1);
        }

        private void Trans_right(object sender, RoutedEventArgs e)
        {
            tr.Trans_obj(sender, e, 1);
        }

        private void Trans_left(object sender, RoutedEventArgs e)
        {
            tr.Trans_obj(sender, e, 0);
        }
        private void Trans_up(object sender, RoutedEventArgs e)
        {
            tr.Trans_obj(sender, e, 2);
        }

        private void Trans_down(object sender, RoutedEventArgs e)
        {
            tr.Trans_obj(sender, e, 3);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            tr = new Trackball();
            tr.EventSource = this.viewport;// src ev used in track.cs
            this.viewport.Camera.Transform = tr.Transform;

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
        
        MessageBox.Show("Please choose point location");
       frst_point = 1;

           }

        int frst_point = 0;
      



        public void MouseHitTest(object sender, MouseButtonEventArgs args)
        {
        
            if (frst_point == 1)//the buton add was clicked befor click on the object 
            {
                r = 0;
            System.Windows.Point mousePos = args.GetPosition(viewport);
            PointHitTestParameters hitParams = new PointHitTestParameters(mousePos);
            VisualTreeHelper.HitTest(viewport, null, ResultCallback, hitParams);
          

            }
             frst_point=0;
        }

        int r = 0;
        public HitTestResultBehavior ResultCallback(HitTestResult result)
        {
             // Did we hit 3D?
            RayHitTestResult rayResult = result as RayHitTestResult;
            if (rayResult != null)
            {
                // Did we hit a MeshGeometry3D?
                RayMeshGeometry3DHitTestResult rayMeshResult = rayResult as RayMeshGeometry3DHitTestResult;

                if (rayMeshResult != null)
                {
                    // Yes we did!
                    if (r == 0)
                    {
                        create_3dpoint(rayMeshResult.PointHit.X, rayMeshResult.PointHit.Y, rayMeshResult.PointHit.Z);
                       // MessageBox.Show(r.ToString());
                        r++;
                    }
                    
                }
            }

            return HitTestResultBehavior.Continue;
          
        }


        /// <summary>
        /// //////////////////////////////connexion to server ////////////////////////////////

        public static void save_points_in_db(double x, double y, double z, int frame, string color, SqlConnection myConnection)
        {


            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }


            SqlCommand myCommand = new SqlCommand("INSERT INTO dbo.Points_saved (colx, coly, colz,frame,color) Values ('" + x + "','" + y + "','" + z + "','" + frame + "','" + color + "')", myConnection);

            //    SqlCommand myCommand = new SqlCommand("INSERT INTO dbo.Points_saved (colx, coly, colz,frame,color) Values (0.1,0.3,0.5,1,'red')", myConnection);

            Console.WriteLine(myCommand.ToString());
            myCommand.ExecuteNonQuery();
 

            try
            {

                myConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        public static String[,] get_points_from_db(int frame, SqlConnection myConnection)
        {
            int i = 0;
            String[,] cordinate = new String[50, 50];
            try
            {
                SqlDataReader myReader = null;

                myConnection.Open();

                SqlCommand myCommand = new SqlCommand("select * from Points_saved where frame=" + frame, myConnection);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {

                    cordinate[i, 0] = myReader["colx"].ToString();
                    cordinate[i, 1] = myReader["coly"].ToString();
                    cordinate[i, 2] = myReader["colz"].ToString();
                    cordinate[i, 3] = myReader["frame"].ToString();
                    cordinate[i, 4] = myReader["color"].ToString();
                    i++;
                }
                myConnection.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }




            return cordinate;


        }

   

         //////////////retrive points///////////
        private void retrive_point(string x, string y, string z,string frame,string color)
        {
            double X, Y, Z;
           // int framee;
            X = Convert.ToDouble(x);
            Y = Convert.ToDouble(y);
            Z = Convert.ToDouble(z);

            SolidColorBrush col;
            // Geometry creation
            col = System.Windows.Media.Brushes.AliceBlue;

            if (color == "#FFFF0000")
            {

              
                col = System.Windows.Media.Brushes.Red;
            }
            else if (color == "#FF0000FF")
            {
                 
                col = System.Windows.Media.Brushes.Blue;

            }
            mGeometry = new GeometryModel3D(Tessellate(X, Y, Z), new DiffuseMaterial(col));
            mGeometry.Transform = new Transform3DGroup();
            group.Children.Add(mGeometry);

            ////////////////insert into Database///////////////////////

           

        }

    }

   

 



}

