using Android.App;
using Plugin.TextToSpeech;
using Android.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Gms.Vision;
using Android.Gms.Vision.Texts;
using Android.Util;
using Android.Graphics;
using Android.Runtime;
using Android;
using Android.Content.PM;
using Android.OS;
using static Android.Gms.Vision.Detector;
using System.Text;
using Android.Support.V4.App;

namespace Eyecam
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, ISurfaceHolderCallback, IProcessor
    {
        private SurfaceView cameraView;
        private TextView textView;
        private CameraSource cameraSource;
        private const int RequestCameraPermissionID = 1001;

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            switch (requestCode)
            {
                case RequestCameraPermissionID:
                    {
                        if (grantResults[0] == Permission.Granted)
                        {
                            cameraSource.Start(cameraView.Holder);
                        }
                    }
                    //new 
                    break;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            cameraView = FindViewById<SurfaceView>(Resource.Id.surface_view);
            textView = FindViewById<TextView>(Resource.Id.text_view);
            
            TextRecognizer textRecognizer = new TextRecognizer.Builder(ApplicationContext).Build();
            if (!textRecognizer.IsOperational)
            {
                Log.Error("Main Activity", "Detector dependencies are not yet available");
            }
            else
            {
                cameraSource = new CameraSource.Builder(ApplicationContext, textRecognizer)
                    .SetFacing(CameraFacing.Back)
                    .SetRequestedPreviewSize(1280, 720)
                    .SetAutoFocusEnabled(true)
                    .Build();
                cameraView.Holder.AddCallback(this);
                textRecognizer.SetProcessor(this);
            }
        }

        public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
        {
            
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            if (ActivityCompat.CheckSelfPermission(ApplicationContext, Manifest.Permission.Camera) != Android.Content.PM.Permission.Granted)
            {
                // request camera permission
                ActivityCompat.RequestPermissions(this, new string[]  {
                    Android.Manifest.Permission.Camera
                }, RequestCameraPermissionID);
                return;
            }
            cameraSource.Start(cameraView.Holder);
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            cameraSource.Stop();
        }

        public void ReceiveDetections(Detections detections)
        {
            SparseArray items = detections.DetectedItems;
            if (items.Size() != 0)
            {
                textView.Post(() => {
                    StringBuilder strBuilder = new StringBuilder();
                    for (int i = 0; i< items.Size(); i++)
                    {
                        strBuilder.Append(((TextBlock)items.ValueAt(i)).Value);
                        strBuilder.Append("\n");
                    }
                    textView.Text = strBuilder.ToString();
                    
                    //text to speech
                    var text = strBuilder.ToString();
                    CrossTextToSpeech.Current.Speak(text);
                });
            }
        }

        public void Release()
        {
            
        }
    }
}

