using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New ParticipantSO", menuName = "Scriptable Objects/New ParticipantSO")]
public class ParticipantSO : ScriptableObject
{
    public List<string> DefaultNames = new();

    [Header("Ayarlar")]
    public string FileName = "OyuncuNickleri.txt";


    [ContextMenu("Dosyadan Ýsimleri Çek")]
    public void LoadNamesFromTextFile()
    {
        // Dosya yolunu oluþtur (Assets/OyuncuNickleri.txt)
        string filePath = Path.Combine(Application.dataPath, FileName);

        if (File.Exists(filePath))
        {
            // Eski listeyi temizle
            DefaultNames.Clear();

            // Dosyadaki her satýrý oku
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    DefaultNames.Add(line.Trim());
                }
            }

            Debug.Log($"{DefaultNames.Count} adet isim baþarýyla yüklendi.");

#if UNITY_EDITOR
            // Deðiþikliklerin kaydedilmesi için SO'yu kirli (dirty) olarak iþaretle
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
#endif
        }
        else
        {
            Debug.LogError($"Dosya bulunamadý! Lütfen Assets klasöründe '{FileName}' olduðundan emin olun. Yol: {filePath}");
        }
    }
}
