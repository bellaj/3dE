












 an open source project that demonstrate how to use a 3d model in WPF application.

the 3d model could be made using any 3d software like maya or blender.

in this project i used a 3d model of an ear for a medical application, 
the user could manipulate the 3d object (zoom, rotate, ...) and add marks over the object.

the 3d object used in this project could be remplaced by any other model 



In the SQL server 
////////////////
1- creat a Database : ear
2-creat a table : Points_saved
3- the Points_saved table contains : 4 coulouns 

colx  
coly
colz
frame
color

the tpye for these coloumns is  varchar40
////////////////// 
In the V studio 
in the Mainwindwo.xaml.cs 

change the connexion string     myConnection = new SqlConnection("Server=KHALIDBELLAJ-PC;Database=ear;Trusted_Connection=True");

by your information(server...)

-------------

chen you start compilation you can add any point and when you restart again you ll get the previous points

---------------Tools---
Microsoft visual studio 2012
4.5 .Net Framwork
Blind Expression

------------------
prerequisites

Windows 7 or higher
4 .Net Framework

-------------how i develop this software-----------


First I generated Xaml file using blind of the Maya Scene object. 
second I imported the resulted Xaml file into your WPF.


                <ModelVisual3D x:Name="ear_int_pCylinder5" >
                    <ModelVisual3D.Content>
                        <GeometryModel3D x:Name="ear_fullmodel_initialShadingGroup1" d:Bounds="-9.16475391387939,-46.0683822631836,-27.6348533630371,35.7756147384644,99.5968208312988,78.9489212036133">
                            <GeometryModel3D.Geometry>
                                <MeshGeometry3D Normals="0.74546599,0.26000601,0.61374098 0.68966597,0.226951,0.687644 0.77292198,0.39004099,0.50046003 0.81725901,0.430608,0.38296801 0.61830401,0.087341003,0.78107101 0.73229402,0.220073,0.64444798 0.83498901,0.34669399,0.427313 0.81864899,0.50973803,0.26453999 0.81468803,0.55462497,0.16933601 0.55887598,-0.062263999,0.82691002 0.66710699,0.016923999,0.74476999 0.50940102,-0.162306,0.84508502 0.59502202,-0.106298,0.79664898
0.69070601,-0.0088980002,0.72308099 0.789868,0.138761,0.597372 0.85030699,0.30978,0.425457 0.85706401,0.46028399,0.231472 0.77383798,0.131975,0.61948103 0.82676899,0.26487201,0.496281 0.83007801,0.481617,0.28109699 0.80793798,0.57664698,0.121306 0.82365298,0.55680698,0.107525 0.80784398,0.58583498,0.064689003 0.80163503,0.59575301,0.049594998 0.807006,0.58949298,0.035218 0.58940601,0.53388602,0.60627198
0.69771397,0.201435,0.687473 0.71534598,-0.124307,0.68762499 

  </GeometryModel3D.Geometry>
                            <GeometryModel3D.Material >
                                <MaterialGroup>
                                    <EmissiveMaterial Brush="Black"/>
                                    <DiffuseMaterial Brush="sc#1, 0.5, 0.5, 0.5"/>
                                    <SpecularMaterial Brush="#00000000" SpecularPower="0"/>
                                </MaterialGroup>
                            </GeometryModel3D.Material>
                        </GeometryModel3D>
                    </ModelVisual3D.Content>
                </ModelVisual3D>



I write some class to move and control the object and print point over it.


------------Important Project files ----------

MainWindow.xaml =  the XAML file which describe the 3d scene. to add or remove object from the scene we have to edit it.
MainWindow.xaml.cs = c# file contain the MainWindow() that initiate the WPF interface, and other functions that create point and handle button click event.
Trackball.cs = define a Trackball class that help us to move rotate and zoom any 3d object.

--------------------MainWindow.xaml.cs functions -------------
create_cube(double x,double y, double z)   add 3d point  to the scene using a x,y,z coordinates of the point.

 Tessellate(double x,double y,double z) // draw the 3d object using its x,y,z


 BuildSolid() // this function create a new trackball to give us the possibility to use the trackball class, which allow us to control the 3object and moving the 3d camera.


public class Freel : INotifyPropertyChanged //unused class(deprecated)

move_left // function that handle left button press on the WPf interface

Button_Click_2 //  initiate a new trackball object to remove previous 3d movement and reinitialise it.

MouseHitTest // this function is executed when the "add point" button is pressed. 
it give us the 3d coordinates of the click point over the 3d object. this function has a callback which is HitTestResultBehavior ResultCallback(HitTestResult result).

HitTestResultBehavior ResultCallback(HitTestResult result)// this function pass the 3d point click coordinates to the create_cube function to create the 3points.



--------------------trackball.cs functions -------------


void move_down(object sender, RoutedEventArgs e) // zoom  and scale using the middle mouse button

public void click(object sender, RoutedEventArgs e,int direction)// rotate the object .The rotation angle is determined by the direction value .UP or Down movement.
 
public void left_right(object sender, RoutedEventArgs e, int direction) rotate the object to left or right.
	
public void btn_zoom(object sender, RoutedEventArgs e,int i) // Zoom function

 public void Trans_obj(object sender, RoutedEventArgs e, int i)// translate object up/down/left/right
	 
/////////////////////////////////////		  
