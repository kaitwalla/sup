using CozyClubhouse.CameraSystem;
using CozyClubhouse.Interaction;
using CozyClubhouse.Player;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace CozyClubhouse.Editor
{
    public static class CozyPrototypeBuilder
    {
        [MenuItem("Cozy Clubhouse/Build Prototype Scene")]
        public static void Build()
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            var ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            ground.name = "Ground";
            ground.transform.position = new Vector3(0f, -0.25f, 0f);
            ground.transform.localScale = new Vector3(12f, 0.5f, 10f);

            var player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            player.name = "Player";
            player.transform.position = new Vector3(0f, 1f, 0f);
            Object.DestroyImmediate(player.GetComponent<CapsuleCollider>());
            player.AddComponent<CharacterController>();
            player.AddComponent<CozyPlayerController>();

            var cameraGo = new GameObject("Main Camera");
            var camera = cameraGo.AddComponent<Camera>();
            cameraGo.tag = "MainCamera";
            camera.fieldOfView = 35f;
            cameraGo.transform.position = new Vector3(8f, 9f, -8f);
            var diorama = cameraGo.AddComponent<DioramaCamera>();
            diorama.SetTarget(player.transform);

            var lightGo = new GameObject("Warm Key Light");
            var light = lightGo.AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 1.4f;
            light.transform.rotation = Quaternion.Euler(48f, -35f, 0f);

            var couch = MakeFurniture("Couch Social Spot", new Vector3(-2.6f, 0.5f, 1.6f), new Vector3(2.8f, 1f, 1.1f));
            AddInteractable(couch, "Chatting", new Vector3(-2.6f, 1f, 0.8f), Quaternion.Euler(0f, 180f, 0f));

            var desk = MakeFurniture("Desk Focus Spot", new Vector3(2.6f, 0.65f, 1.8f), new Vector3(2.4f, 1.3f, 0.8f));
            AddInteractable(desk, "Focusing", new Vector3(2.6f, 1f, 0.8f), Quaternion.Euler(0f, 180f, 0f));

            MakeFurniture("Low Table", new Vector3(-0.7f, 0.3f, 2.2f), new Vector3(1.4f, 0.6f, 1.1f));
            MakeFurniture("Plant Placeholder", new Vector3(4.1f, 0.8f, -2.4f), new Vector3(0.8f, 1.6f, 0.8f));

            const string scenePath = "Assets/CozyClubhouse/Scenes/CozyPrototype.unity";
            EnsureFolder("Assets/CozyClubhouse/Scenes");
            EditorSceneManager.SaveScene(scene, scenePath);
            Selection.activeGameObject = player;
            Debug.Log($"Built cozy prototype scene at {scenePath}");
        }

        private static GameObject MakeFurniture(string name, Vector3 position, Vector3 scale)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = name;
            go.transform.position = position;
            go.transform.localScale = scale;
            return go;
        }

        private static void AddInteractable(GameObject target, string activity, Vector3 position, Quaternion rotation)
        {
            var point = new GameObject("Snap Point").transform;
            point.SetParent(target.transform);
            point.position = position;
            point.rotation = rotation;

            var interactable = target.AddComponent<CozyInteractable>();
            var serialized = new SerializedObject(interactable);
            serialized.FindProperty("activity").stringValue = activity;
            serialized.FindProperty("snapPoint").objectReferenceValue = point;
            serialized.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void EnsureFolder(string path)
        {
            var parts = path.Split('/');
            var current = parts[0];
            for (int i = 1; i < parts.Length; i++)
            {
                var next = current + "/" + parts[i];
                if (!AssetDatabase.IsValidFolder(next)) AssetDatabase.CreateFolder(current, parts[i]);
                current = next;
            }
        }
    }
}
