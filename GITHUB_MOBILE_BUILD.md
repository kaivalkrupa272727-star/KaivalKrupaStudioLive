# Mobile-only GitHub build

આ ZIPમાં GitHub Actions workflow પહેલેથી તૈયાર છે.

1. GitHubમાં નવું/ખાલી repository બનાવો.
2. ZIP extract કરીને તેની બધી files repositoryની `main` branchમાં upload કરો.
3. `.github/workflows/build-windows.yml` fileને બદલશો નહીં.
4. Upload પછી GitHub Actions આપમેળે Windows build શરૂ કરશે. `Actions` → `Build KaivalKrupa Studio Live V3` → latest run ખોલો.
5. Run green થયા પછી `Artifacts`માં `KaivalKrupaStudioLive_V3_Windows_x64` ZIP download કરો.
6. Download કરેલી ZIP extract કરીને `SpotLiveV3.exe` ચલાવો.

નોંધ: આ project .NET 8 WPF છે. Streaming માટે `ffmpeg.exe` અલગથી app folderમાં રાખવું જરૂરી છે.
