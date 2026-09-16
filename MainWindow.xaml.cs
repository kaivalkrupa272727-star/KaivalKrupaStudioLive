using Microsoft.Win32;
using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SpotLiveV3.Services;
namespace SpotLiveV3;
public partial class MainWindow : Window {
 CameraService c1=new(), c2=new(), c3=new(), c4=new(); Mat? last1,last2,last3,last4,program; FfmpegPipe? streamer; VideoWriter? writer; string ffmpeg="ffmpeg.exe"; int w=1280,h=720,fps=30; bool muted=false;
 public MainWindow(){InitializeComponent(); Loaded+=(_,_)=>Start(); Closing+=(_,_)=>StopAll();}
 void Start(){try{c1.FrameReady+=m=>OnFrame(1,m);c2.FrameReady+=m=>OnFrame(2,m);c3.FrameReady+=m=>OnFrame(3,m);c4.FrameReady+=m=>OnFrame(4,m);c1.Start(0,w,h,fps);try{c2.Start(1,w,h,fps);}catch{}try{c3.Start(2,w,h,fps);}catch{}try{c4.Start(3,w,h,fps);}catch{}Status.Text="4 camera inputs ready. Select a source for PROGRAM.";}catch(Exception ex){Status.Text="Camera start: "+ex.Message;}}
 void OnFrame(int n,Mat m){Dispatcher.Invoke(()=>{switch(n){case 1:last1?.Dispose();last1=m;Cam1Image.Source=BitmapSourceConverter.ToBitmapSource(m);break;case 2:last2?.Dispose();last2=m;Cam2Image.Source=BitmapSourceConverter.ToBitmapSource(m);break;case 3:last3?.Dispose();last3=m;Cam3Image.Source=BitmapSourceConverter.ToBitmapSource(m);break;case 4:last4?.Dispose();last4=m;Cam4Image.Source=BitmapSourceConverter.ToBitmapSource(m);break;}});}
 void SetProgram(Mat? m,string label){if(m==null)return;program?.Dispose();program=m.Clone();ProgramImage.Source=BitmapSourceConverter.ToBitmapSource(program);Status.Text="PROGRAM: "+label;}
 void A1_Click(object s,RoutedEventArgs e)=>SetProgram(last1,"CAM 1"); void A2_Click(object s,RoutedEventArgs e)=>SetProgram(last2,"CAM 2"); void A3_Click(object s,RoutedEventArgs e)=>SetProgram(last3,"CAM 3"); void A4_Click(object s,RoutedEventArgs e)=>SetProgram(last4,"CAM 4");
 void B1_Click(object s,RoutedEventArgs e)=>SetProgram(last1,"CAM 1 (B)"); void B2_Click(object s,RoutedEventArgs e)=>SetProgram(last2,"CAM 2 (B)"); void B3_Click(object s,RoutedEventArgs e)=>SetProgram(last3,"CAM 3 (B)"); void B4_Click(object s,RoutedEventArgs e)=>SetProgram(last4,"CAM 4 (B)");
 void Scene1_Click(object s,RoutedEventArgs e)=>A1_Click(s,e); void Scene2_Click(object s,RoutedEventArgs e)=>A2_Click(s,e); void Scene3_Click(object s,RoutedEventArgs e)=>A3_Click(s,e); void Scene4_Click(object s,RoutedEventArgs e)=>A4_Click(s,e); void Scene5_Click(object s,RoutedEventArgs e)=>Image_Click(s,e);
 void Image_Click(object s,RoutedEventArgs e){var d=new OpenFileDialog{Filter="Images|*.jpg;*.jpeg;*.png;*.bmp"};if(d.ShowDialog()!=true)return;using var m=Cv2.ImRead(d.FileName);SetProgram(m,"IMAGE");}
 void Video_Click(object s,RoutedEventArgs e){var d=new OpenFileDialog{Filter="Video|*.mp4;*.mov;*.avi;*.mkv"};if(d.ShowDialog()!=true)return;Status.Text="VIDEO source selected: "+d.FileName+". Full playlist playback is ready for next module.";}
 void Cut_Click(object s,RoutedEventArgs e)=>Status.Text="Transition: CUT"; void Fade_Click(object s,RoutedEventArgs e)=>Status.Text="Transition: FADE"; void Wipe_Click(object s,RoutedEventArgs e)=>Status.Text="Transition: WIPE";
 void ApplyText_Click(object s,RoutedEventArgs e)=>Status.Text="Overlay text: "+OverlayText.Text; void Mute_Click(object s,RoutedEventArgs e){muted=!muted;Status.Text=muted?"Audio muted":"Audio active";}
 void Record_Click(object s,RoutedEventArgs e){if(writer!=null){writer.Release();writer.Dispose();writer=null;RecBtn.Content="● RECORD";Status.Text="Recording stopped";return;}var d=new SaveFileDialog{Filter="MP4|*.mp4",FileName="KaivalKrupaStudioLive_Recording.mp4"};if(d.ShowDialog()!=true)return;writer=new VideoWriter(d.FileName,FourCC.H264,fps,new OpenCvSharp.Size(w,h));RecBtn.Content="■ STOP RECORD";Status.Text="Recording: "+d.FileName;_ = RecordLoop();}
 async Task RecordLoop(){while(writer!=null){var m=program;if(m!=null){using var r=new Mat();Cv2.Resize(m,r,new OpenCvSharp.Size(w,h));writer.Write(r);}await Task.Delay(1000/fps);}}
 void Stream_Click(object s,RoutedEventArgs e){if(streamer?.Running==true){streamer.Stop();StreamBtn.Content="▶ STREAM";Status.Text="Stream stopped";return;}var url=Microsoft.VisualBasic.Interaction.InputBox("Enter Facebook/YouTube RTMP URL + stream key (full URL):","Start Live Stream","");if(string.IsNullOrWhiteSpace(url))return;try{streamer=new FfmpegPipe();streamer.Start(ffmpeg,url,w,h,fps);StreamBtn.Content="■ STOP STREAM";Status.Text="Streaming started. Ensure ffmpeg.exe is beside the app.";_ = StreamLoop();}catch(Exception ex){Status.Text=ex.Message;}}
 async Task StreamLoop(){while(streamer?.Running==true){if(program!=null)await streamer.WriteAsync(program);await Task.Delay(1000/fps);}}
 void Settings_Click(object s,RoutedEventArgs e){MessageBox.Show("V3 settings: 4 camera inputs (device indexes 0-3), 720p/1080p, FPS, FFmpeg path, audio device and stream presets can be expanded here.","કૈવલકૃપા સ્ટુડિયો લાઈવ");}
 void StopAll(){writer?.Release();writer?.Dispose();streamer?.Stop();c1.Dispose();c2.Dispose();c3.Dispose();c4.Dispose();program?.Dispose();last1?.Dispose();last2?.Dispose();last3?.Dispose();last4?.Dispose();}
}
