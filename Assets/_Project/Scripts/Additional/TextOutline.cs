using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Additional
{
	public class TextOutline : MonoBehaviour 
	{
		[FormerlySerializedAs("pixelSize")] [SerializeField] private float _pixelsSize = 1;
		[FormerlySerializedAs("outlineColor")] [SerializeField] private Color _colorOutline = Color.black;
		private bool _isResolution;
		private int _doubleResolution = 1024;
		private TextMesh _mesh;
		private MeshRenderer _meshRenderer;

		private void Start() 
		{
			_mesh = GetComponent<TextMesh>();    
			_meshRenderer = GetComponent<MeshRenderer>();
		
			for (int i = 0; i < 8; i++) {
				GameObject outline = new GameObject("outline", typeof(TextMesh));
				outline.transform.parent = transform;
				outline.transform.localScale = new Vector3(1, 1, 1);
				outline.AddComponent<SortOrder>();
				MeshRenderer otherMeshRenderer = outline.GetComponent<MeshRenderer>();
				otherMeshRenderer.material = new Material(_meshRenderer.material);
				otherMeshRenderer.castShadows = false;
				otherMeshRenderer.receiveShadows = false;
				otherMeshRenderer.sortingLayerID = _meshRenderer.sortingLayerID;
				otherMeshRenderer.sortingLayerName = _meshRenderer.sortingLayerName;
			}
		}

		private void LateUpdate() 
		{
			Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
			_colorOutline.a = _mesh.color.a * _mesh.color.a;
			for (int i = 0; i < transform.childCount; i++) {
			
				TextMesh other = transform.GetChild(i).GetComponent<TextMesh>();
				other.color = _colorOutline;
				other.text = _mesh.text;
				other.alignment = _mesh.alignment;
				other.anchor = _mesh.anchor;
				other.characterSize = _mesh.characterSize;
				other.font = _mesh.font;
				other.fontSize = _mesh.fontSize;
				other.fontStyle = _mesh.fontStyle;
				other.richText = _mesh.richText;
				other.tabSize = _mesh.tabSize;
				other.lineSpacing = _mesh.lineSpacing;
				other.offsetZ = _mesh.offsetZ;
			
				bool doublePixel = _isResolution && (Screen.width > _doubleResolution || Screen.height > _doubleResolution);
				Vector3 pixelOffset = Offset(i) * (doublePixel ? 2.0f * _pixelsSize : _pixelsSize);
				Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint + pixelOffset);
				other.transform.position = worldPoint;
			
				MeshRenderer otherMeshRenderer = transform.GetChild(i).GetComponent<MeshRenderer>();
				otherMeshRenderer.sortingLayerID = _meshRenderer.sortingLayerID;
				otherMeshRenderer.sortingLayerName = _meshRenderer.sortingLayerName;
			}
		}

		private static Vector3 Offset(int i) 
		{
			switch (i % 8) {
				case 0: return new Vector3(0, 1, 0);
				case 1: return new Vector3(1, 1, 0);
				case 2: return new Vector3(1, 0, 0);
				case 3: return new Vector3(1, -1, 0);
				case 4: return new Vector3(0, -1, 0);
				case 5: return new Vector3(-1, -1, 0);
				case 6: return new Vector3(-1, 0, 0);
				case 7: return new Vector3(-1, 1, 0);
				default: return Vector3.zero;
			}
		}
	}
}