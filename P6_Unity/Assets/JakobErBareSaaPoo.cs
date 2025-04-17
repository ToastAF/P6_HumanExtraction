using UnityEngine;
using System.Text;
using UMA;

#if UNITY_EDITOR
using UnityEditor;

    public class JakobErBareSaaPoo : MonoBehaviour
    {

        public GameObject male;
        public GameObject female;
        public void ExportActiveAvatarToObj()
        {

            GameObject activeGO = null;

            if (male != null && male.activeInHierarchy)
            {
                activeGO = male;
            }
            else if (female != null && female.activeInHierarchy)
            {
                activeGO = female;
            }

            if (activeGO == null)
            {
                Debug.LogWarning("No active avatar found (neither male nor female is active).");
                return;
            }

            var selectedTransform = activeGO.transform;
            var avatar = selectedTransform.GetComponent<UMAAvatarBase>();
            while (avatar == null && selectedTransform.parent != null)
            {
                selectedTransform = selectedTransform.parent;
                avatar = selectedTransform.GetComponent<UMAAvatarBase>();
            }

            if (avatar != null)
            {
                var path = EditorUtility.SaveFilePanel("Save obj static mesh", "Assets", avatar.name + ".obj", "obj");
                if (path.Length != 0)
                {
                    var staticMesh = new Mesh();
#if UMA_32BITBUFFERS
                    staticMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
#endif
                    avatar.umaData.GetRenderer(0).BakeMesh(staticMesh);
                    FileUtils.WriteAllText(path, MeshToString(staticMesh, avatar.umaData.GetRenderer(0).sharedMaterials));
                    UMAUtils.DestroySceneObject(staticMesh);
                    Debug.Log($"Exported {avatar.name} to {path}");
                }
            }
            else
            {
                Debug.LogWarning("UMAAvatarBase not found on active GameObject.");
            }
        }

        private string MeshToString(Mesh mesh, Material[] materials)
        {
            Mesh m = mesh;
            Material[] mats = materials;

            StringBuilder sb = new StringBuilder();

            sb.Append("g ").Append(m.name).Append("\n");
            foreach (Vector3 v in m.vertices)
            {
                sb.Append(string.Format("v {0} {1} {2}\n", v.x, v.y, v.z));
            }
            sb.Append("\n");
            foreach (Vector3 v in m.normals)
            {
                sb.Append(string.Format("vn {0} {1} {2}\n", v.x, v.y, v.z));
            }
            sb.Append("\n");
            foreach (Vector3 v in m.uv)
            {
                sb.Append(string.Format("vt {0} {1}\n", v.x, v.y));
            }
            for (int material = 0; material < m.subMeshCount; material++)
            {
                sb.Append("\n");
                sb.Append("usemtl ").Append(mats[material].name).Append("\n");
                sb.Append("usemap ").Append(mats[material].name).Append("\n");

                int[] triangles = m.GetTriangles(material);
                for (int i = 0; i < triangles.Length; i += 3)
                {
                    sb.Append(string.Format("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}\n",
                        triangles[i] + 1, triangles[i + 1] + 1, triangles[i + 2] + 1));
                }
            }
            return sb.ToString();
        }
    }
#endif