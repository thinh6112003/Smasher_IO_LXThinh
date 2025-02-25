//using UnityEngine;
//using UnityEditor;

//public class TextureCompressor : EditorWindow
//{
//    [MenuItem("Tools/Texture Compressor")]
//    public static void ShowWindow()
//    {
//        GetWindow<TextureCompressor>("Texture Compressor");
//    }

//    private void OnGUI()
//    {
//        if (GUILayout.Button("Compress Textures for Android"))
//        {
//            CompressTextures();
//        }
//    }

//    private static void CompressTextures()
//    {
//        // Lấy tất cả các texture trong project
//        string[] guids = AssetDatabase.FindAssets("t:Texture2D");

//        int processedCount = 0; // Đếm số lượng texture đã xử lý
//        foreach (string guid in guids)
//        {
//            string path = AssetDatabase.GUIDToAssetPath(guid);

//            // Bỏ qua nếu file nằm trong thư mục Packages
//            if (path.StartsWith("Packages"))
//            {
//                continue;
//            }

//            // Load texture importer
//            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
//            if (importer == null)
//                continue;

//            // Kiểm tra nếu texture đã nén đúng định dạng
//            TextureImporterPlatformSettings androidSettings = importer.GetPlatformTextureSettings("Android");

//            bool isCrunched = androidSettings.format == TextureImporterFormat.ETC2_RGBA8Crunched;
//            bool isASTC = androidSettings.format == TextureImporterFormat.ASTC_8x8;

//            if (isCrunched || isASTC)
//            {
//                continue; // Đã đúng định dạng, bỏ qua
//            }

//            // Load texture để lấy thông tin kích thước
//            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
//            if (texture == null)
//                continue;

//            int width = texture.width;
//            int height = texture.height;

//            // Gán format theo điều kiện
//            androidSettings.overridden = true;
//            if (width % 4 == 0 && height % 4 == 0)
//            {
//                androidSettings.format = TextureImporterFormat.ETC2_RGBA8Crunched; // RGBA Crunched ETC2
//            }
//            else
//            {
//                androidSettings.format = TextureImporterFormat.ASTC_8x8; // RGBA Compressed ASTC 8x8
//            }

//            importer.SetPlatformTextureSettings(androidSettings);

//            // Áp dụng thay đổi
//            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
//            processedCount++;
//        }

//        Debug.Log($"Texture compression complete! Total textures processed: {processedCount}");
//    }
//}