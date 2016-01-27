#3DE Project#

3DE project is a WPF application manipulating a 3d object (in this project, i used a 3d model of an ear for a medical application). 
the users (doctors or medicine students) could manipulate the 3d object (zoom, rotate, ...) and add points(labels) over the object. the 3d model used in this project could be remplaced by any other objetc and it could be adapted to many fields. 
The project ***is opensource***, please feel free to hack it and to give me your feedback, ****please refer this project in case you use it****.

##screenshots##

![alt tag](http://i66.tinypic.com/11l7y4w.png)

![alt tag](http://i64.tinypic.com/x1d20g.png)

###General###
if you want to manipulate a 3d object in a WPF you have to choose between :

    *Approach. You are in interest to put some .3ds(.obj,..) model object as stationary part of your scene without any interactive transformations (moving, scaling and so on). This approach is for simple playing (learning WPF3D) as a rule
    *Approach. You are thinking to have full interactive part with support any WPF3D transformations within your Viewport3D. This approach is for rich 3D scene manipulations in professional application as a rule. 
    
    in 3dE project I demonstrate how to use a 3d model in WPF application using ModelVisual3D. the 3d model could be made using any 3d software like maya or blender.


###Tools & prerequisites###
<ol start="1">
 <li>Microsoft visual studio 2012</li>
 <li>4.5 .Net Framwork</li>
 <li>Blind Expression</li>
 <li>Windows 7 or higher</li>
 <li>.Net Framework 4</li>
</ol>
###setup###
####Step 1####


In the SQL server import the ear database or creat it :
<ol start="1">
 <li>creat a Database : ear</li>
 <li>creat a table : Points_saved</li>
 <li> the Points_saved table contains : 4 coulouns </ol>
	<ol start="1">
	<li>	colx  </li>
	<li>	coly </li>
	<li>	colz </li>
	<li>	frame </li>
	<li>	color </li>
	</ol>
</li>
the type for these coloumns is  varchar40
</ol>

####step 2 : connexion string edition####

using V studio 2012 open the Mainwindwo.xaml.cs change the connexion string   

myConnection = new SqlConnection("Server=KHALIDBELLAJ-PC;Database=ear;Trusted_Connection=True");

by your information(server...)

####step 3 : compilation ####

clean the project and start the compilation. when the WPF prompt you can manipulate the object and add any points the application will save them for the next start.

#### how i developed this software ####
Very important :
*First* I generated the Xaml file using blind of the Maya Scene object(*.obj). you will get a big list of numbers.
*second* I imported the resulted Xaml file into the WPF XAML, you could insert directly the <ModelVisual3D markup as shown bellow .


                <ModelVisual3D x:Name="ear_int_pCylinder5" >
                    <ModelVisual3D.Content>
                        <GeometryModel3D x:Name="ear_fullmodel_initialShadingGroup1" d:Bounds="-9.16475391387939,-46.0683822631836,-27.6348533630371,35.7756147384644,99.5968208312988,78.9489212036133">
                            <GeometryModel3D.Geometry>
                                <MeshGeometry3D Normals="0.74546599,0.26000601,0.61374098 0.68966597,0.226951,0.687644 0.77292198,0.39004099,0.50046003 0.81725901,0.430608,0.38296801 0.61830401,0.087341003,0.78107101 0.73229402,0.220073,0.64444798 0.83498901,0.34669399,0.427313 0.81864899,0.50973803,0.26453999 0.81468803,0.55462497,0.16933601 0.55887598,-0.062263999,0.82691002 0.66710699,0.016923999,0.74476999 0.50940102,-0.162306,0.84508502 0.59502202,-0.106298,0.79664898,0.69070601,-0.0088980002,0.72308099 0.789868,0.138761,0.597372 0.85030699,0.30978,0.425457 0.85706401,0.049594998 0.807006..............">

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


this represent the Elements of a 3D scene
****Viewport3D**** 
    The viewport is the control that builds the gate between the 2D and the 3D world.
****Camera**** 
    Every 3D scene has excactly one camera. The camera defines the Position and the LookDirection and the UpDirection of the viewer. WPF supports orthographical and perspective cameras.
****3D Models****
    A 3D model defines an object in the scene. It has a Geometry that is the mesh and a Material that can be a diffuse, specular or emmisive material. The material itself has a brush.

In addition I write some class to move and control the object and print point over it.


### DOC : Important Project files###
<ol>
<li>***MainWindow.xaml*** the XAML file which describe the 3d scene. to add or remove object from the scene we have to edit it.</li>
<li>***MainWindow.xaml.cs***  c# file contain the MainWindow() that initiate the WPF interface, and other functions that create point and handle button click event.</li>
<li>***Trackball.cs*** = define a Trackball class that help us to move rotate and zoom any 3d object.</li>
</ol>
####MainWindow.xaml.cs functions ####
<ol>
<li>***create_cube(double x,double y, double z)***   add 3d point  to the scene using a x,y,z coordinates of the point.</li>
<li>***Tessellate(double x,double y,double z)*** draw the 3d object using its x,y,z.</li>
<li>***BuildSolid()*** this function create a new trackball to give us the possibility to use the trackball class, which allow us to control the 3object and moving the 3d camera.</li>
<li>***public class Freel : INotifyPropertyChanged*** //unused class(deprecated)</li>
<li>***move_left*** function that handle left button press on the WPf interface.</li>
<li>***Button_Click_2***  initiate a new trackball object to remove previous 3d movement and reinitialise it.</li>

<li>***MouseHitTest*** this function is executed when the "add point" button is pressed. it gives us the 3d coordinates of the click point over the 3d object. this function has a callback which is HitTestResultBehavior ResultCallback(HitTestResult result).</li>
<li>***HitTestResultBehavior ResultCallback(HitTestResult result)*** this function pass the 3d point click coordinates to the create_cube function to create the 3points.</li>
</ol>
####trackball.cs functions###
<ol>
<li>***void move_down(object sender, RoutedEventArgs e)***   zoom  and scale using the middle mouse button</li>

<li>***public void click(object sender, RoutedEventArgs e,int direction)***  rotate the object .The rotation angle is determined by the direction value .UP or Down movement.</li>
 
<li>***public void left_right(object sender, RoutedEventArgs e, int direction)*** rotate the object to left or right.</li>
	
<li>***public void btn_zoom(object sender, RoutedEventArgs e,int i)***   Zoom function</li>

<li>***public void Trans_obj(object sender, RoutedEventArgs e, int i)***  translate object up/down/left/right</li>
	  
</ol>

more comments are included in the project files
