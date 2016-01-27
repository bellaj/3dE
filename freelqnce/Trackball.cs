 

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Windows.Markup;
using System.Reflection;
using System.Windows.Media;

namespace _3DTools
{
  
    public class Trackball
    {
        private FrameworkElement _eventSource;
        private Point _previousPosition2D;
        private Vector3D _previousPosition3D = new Vector3D(0, 0, 1);
        double ywhe=0;

        private Transform3DGroup _transform;
        private ScaleTransform3D _scale = new ScaleTransform3D();
        private AxisAngleRotation3D _rotation = new AxisAngleRotation3D();
        private TranslateTransform3D _translate = new TranslateTransform3D();
        private Double transScale = 1;//
            //bool create_point = false;

        public Trackball()
        {
            _transform = new Transform3DGroup();
            _transform.Children.Add(_scale);
            _transform.Children.Add(new RotateTransform3D(_rotation));

            _transform.Children.Add(_translate);//added

        }

        /// <summary>
        ///     A transform to move the camera or scene to the trackball's
        ///     current orientation and scale.
        /// </summary>
        public Transform3D Transform
        {
            get { return _transform; }
        }

        #region Event Handling

        /// <summary>
        ///     The FrameworkElement we listen to for mouse events.
        /// </summary>
        public FrameworkElement EventSource
        {
            get { return _eventSource; }
            
            set
            {
                if (_eventSource != null)
                {
                    _eventSource.MouseDown -= this.OnMouseDown;
                    _eventSource.MouseUp -= this.OnMouseUp;
                    _eventSource.MouseMove -= this.OnMouseMove;
                    _eventSource.MouseWheel -= this.MouseWheel;
                  //  _eventSource.buton_clc -= this.buton_clc;
//
                }

                _eventSource = value;
                _eventSource.MouseDown += this.OnMouseDown;
                _eventSource.MouseUp += this.OnMouseUp;
                _eventSource.MouseMove += this.OnMouseMove;
                _eventSource.MouseWheel += this.MouseWheel;
            //    _eventSource.buton_clc -= this.buton_clc;
            }
        }

        public Double TranslateScale
{
get { return transScale; }

set { transScale = value; }
}

 


        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            Mouse.Capture(EventSource, CaptureMode.Element);
            _previousPosition2D = e.GetPosition(EventSource);
            _previousPosition3D = ProjectToTrackball( EventSource.ActualWidth,EventSource.ActualHeight, _previousPosition2D);
            
        }
   


        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            Mouse.Capture(EventSource, CaptureMode.None);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            Point currentPosition = e.GetPosition(EventSource);

            // Prefer tracking to zooming if both buttons are pressed.
            if (e.LeftButton == MouseButtonState.Pressed)
            {
               Track(currentPosition);
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
               Translate(currentPosition);
            }
         
        }



             public void get_pos(){
                 MessageBox.Show(_previousPosition2D.ToString() +"dd" );    
        }

         /*    public void creat_but(bool i) {
                 if (i)
                     create_point = true;
                 else
                     create_point = false;
             }
        */
        private void move_down(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("éed");

        }
        public void MouseWheel(object sender, MouseWheelEventArgs e)//aded
        {

            ywhe = e.Delta;
 
          double scale = Math.Exp(ywhe / 500);    // e^(yDelta/100) is fairly arbitrary.

          _scale.ScaleX *= scale;
          _scale.ScaleY *= scale;
          _scale.ScaleZ *= scale;

        }
        double xp = 0, yp = 0;
        Point currentPositionclic;
         Vector3D pPosition3D = new Vector3D(0, 0, 1);
         double angle=0;
          public void clic(object sender, RoutedEventArgs e,int direction)
        {
                     
                           //         MessageBox.Show(angle.ToString());

              currentPositionclic = new Point(xp,yp);

                       
                                              // MessageBox.Show(currentPositionclic.ToString() + "all" + _previousPosition3D.ToString());

                        Vector3D currentPosition3D = ProjectToTrackball(EventSource.ActualWidth, EventSource.ActualHeight, currentPositionclic);

                        Vector3D axis = new Vector3D(0, 0, 1);
                        if (direction == 1)//up
                        {
                            if (angle < 0)
                                angle = 0;
                            angle += 3;// Vector3D.AngleBetween(pPosition3D, currentPosition3D);
                        }
                        else//up
                        {
                            if (angle > 0) angle = 0;
                            angle -= 3;
                        }
                        Quaternion delta = new Quaternion(axis, -angle);


                        // Get the current orientantion from the RotateTransform3D
                        AxisAngleRotation3D r = _rotation;
                        Quaternion q = new Quaternion(_rotation.Axis, _rotation.Angle);

                        // Compose the delta with the previous orientation
                        q *= delta;

                        // Write the new orientation back to the Rotation3D
                        _rotation.Axis = q.Axis;
                        //MessageBox.Show(_rotation.Axis.ToString());
                        _rotation.Angle = q.Angle;

                        _previousPosition3D = currentPosition3D;
                        yp += 1; xp += 1;




        }


          public  void get_position(object sender, MouseButtonEventArgs e,Viewport3DVisual v)
          {

              Point pt = e.GetPosition((UIElement)sender);

              // Perform the hit test against a given portion of the visual object tree.
              HitTestResult result = VisualTreeHelper.HitTest(v, pt);


              if (result != null)
              {
                  // Perform action on hit visual object.
              }

          }


 



          public void left_right(object sender, RoutedEventArgs e, int direction)
          {

              //         MessageBox.Show(angle.ToString());

              currentPositionclic = new Point(xp, yp);


              // MessageBox.Show(currentPositionclic.ToString() + "all" + _previousPosition3D.ToString());

              Vector3D currentPosition3D = ProjectToTrackball(EventSource.ActualWidth, EventSource.ActualHeight, currentPositionclic);

              Vector3D axis = new Vector3D(0, 1, 0);
              if (direction == 1)//up
              {
                  if (angle < 0)
                      angle = 0;
                  angle += 3;// Vector3D.AngleBetween(pPosition3D, currentPosition3D);
              }
              else//up
              {
                  if (angle > 0) angle = 0;
                  angle -= 3;
              }
              Quaternion delta = new Quaternion(axis, -angle);


              // Get the current orientantion from the RotateTransform3D
              AxisAngleRotation3D r = _rotation;
              Quaternion q = new Quaternion(_rotation.Axis, _rotation.Angle);

              // Compose the delta with the previous orientation
              q *= delta;

              // Write the new orientation back to the Rotation3D
              _rotation.Axis = q.Axis;
              //MessageBox.Show(_rotation.Axis.ToString());
              _rotation.Angle = q.Angle;

              _previousPosition3D = currentPosition3D;
              yp += 1; xp += 1;




          }


          public void btn_zoom(object sender, RoutedEventArgs e,int i)
          {

              if (i == 1)
              {
                  if (ywhe >0) ywhe = 0;
                  ywhe = ywhe - 1;
              }
              else
              { if (ywhe < 0) ywhe = 0; ywhe = ywhe + 1; }

        double scale = Math.Exp(ywhe / 100);    // e^(yDelta/100) is fairly arbitrary.
            // MessageBox.Show(scale.ToString());

              _scale.ScaleX *= scale;
              _scale.ScaleY *= scale;
              _scale.ScaleZ *= scale;  /*    */

            
          }

          public void Trans_obj(object sender, RoutedEventArgs e, int i)
          {
              if(i==0)//left
                  _translate.OffsetZ -= 1;
                  if (i == 1)//right
                      _translate.OffsetZ += 1;
                      if (i == 2)//up
                            _translate.OffsetY -= 1; 
                           if (i ==3)
                              _translate.OffsetY += 1;
              //_translate.OffsetX +=1;
              //_translate.OffsetY += 10;
             // _translate.OffsetZ += qV.Z;
          }




        #endregion Event Handling


        


        private void Translate(Point currentPosition)
        {
            // Calculate the panning vector from screen(the vector component of the Quaternion
            // the division of the X and Y components scales the vector to the mouse movement
            Quaternion qV = new Quaternion(((_previousPosition2D.X - currentPosition.X) / transScale), ((currentPosition.Y - _previousPosition2D.Y) / transScale), 0, 0);
           
            // Get the current orientantion from the RotateTransform3D
            Quaternion q = new Quaternion(_rotation.Axis, _rotation.Angle);
            Quaternion qC = q;
            qC.Conjugate();

            // Here we rotate our panning vector about the the rotaion axis of any current rotation transform
            // and then sum the new translation with any exisiting translation
            qV = q * qV * qC;
            _translate.OffsetX += qV.X;
            _translate.OffsetY += qV.Y;
            _translate.OffsetZ += qV.Z;
        }

        private void Track(Point currentPosition)
        {

         //   MessageBox.Show(currentPosition.ToString() + "all" + _previousPosition3D.ToString());

            Vector3D currentPosition3D = ProjectToTrackball(
                EventSource.ActualWidth, EventSource.ActualHeight, currentPosition);

            Vector3D axis = Vector3D.CrossProduct(_previousPosition3D, currentPosition3D);
            double angle = Vector3D.AngleBetween(_previousPosition3D, currentPosition3D);
            Quaternion delta = new Quaternion(axis, -angle);

            // Get the current orientantion from the RotateTransform3D
            AxisAngleRotation3D r = _rotation;
            Quaternion q = new Quaternion(_rotation.Axis, _rotation.Angle);

            // Compose the delta with the previous orientation
            q *= delta;

            // Write the new orientation back to the Rotation3D
            _rotation.Axis = q.Axis;
            _rotation.Angle = q.Angle;
            _previousPosition3D = currentPosition3D;
        }

        private Vector3D ProjectToTrackball(double width, double height, Point point)
        {
            double x = point.X / (width / 2);    // Scale so bounds map to [0,0] - [2,2]
            double y = point.Y / (height / 2);

            x = x - 1;                           // Translate 0,0 to the center
            y = 1 - y;                           // Flip so +Y is up instead of down

            double z2 = 1 - x * x - y * y;       // z^2 = 1 - x^2 - y^2
            double z = z2 > 0 ? Math.Sqrt(z2) : 0;

            return new Vector3D(x, y, z);
        }

        private void Zoom(Point currentPosition)
        {
            double yDelta = currentPosition.Y - _previousPosition2D.Y;
            
            double scale = Math.Exp(yDelta / 100);    // e^(yDelta/100) is fairly arbitrary.

            _scale.ScaleX *= scale;
            _scale.ScaleY *= scale;
            _scale.ScaleZ *= scale;

         
        }
    }
}
