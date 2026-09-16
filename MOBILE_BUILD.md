# મોબાઈલથી EXE બનાવવાની સરળ રીત

1. GitHub પર account બનાવો અને New repository બનાવો. ઉદાહરણ: `KaivalKrupaStudioLive`.
2. આ project ZIP extract કરીને તેની અંદરના બધા files GitHub repositoryમાં upload કરો.
3. ખાતરી કરો કે `.github/workflows/build-windows.yml` file upload થઈ છે.
4. GitHubમાં **Actions** tab ખોલો.
5. **Build KaivalKrupa Studio Live V3** workflow પસંદ કરો.
6. **Run workflow** દબાવો.
7. Build પૂરો થયા પછી workflowના completed runમાં **Artifacts** હેઠળ `KaivalKrupaStudioLive_V3_Windows_x64` download કરો.
8. ZIP extract કરીને Windows PCમાં `.exe` ચલાવો.

નોંધ: Streaming માટે project હાલ `ffmpeg.exe` શોધે છે. તેથી streaming વાપરતી વખતે `ffmpeg.exe` applicationની સાથે રાખવું પડશે. Camera 1-4 સામાન્ય રીતે Windows device indexes 0-3 છે; capture cards પ્રમાણે બદલાઈ શકે છે.
