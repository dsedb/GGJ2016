using UnityEngine;
using System.Collections;

[RequireComponent (typeof (MeshFilter))]
[RequireComponent (typeof (MeshRenderer))]
public class PaperEffect : MonoBehaviour
{
	private const float width = 0.667f;
	private const float height = 2f;
	private const int X_NUM = 16;
	private const int Y_NUM = 32;

	void Awake()
	{
		var vertices = new Vector3[X_NUM * Y_NUM];
		for (int y = 0; y < Y_NUM; ++y) {
			for (int x = 0; x < X_NUM; ++x) {
				vertices[x + y * X_NUM] = new Vector3 ((x*width/X_NUM) - width*0.5f, (y*height/Y_NUM) - height*0.5f, 0);
			}
		}
		var uvs = new Vector2[X_NUM * Y_NUM];
		for (int y = 0; y < Y_NUM; ++y) {
			for (int x = 0; x < X_NUM; ++x) {
				uvs [x + y * X_NUM] = new Vector2 (((float)x) / X_NUM, ((float)(y-(Y_NUM/4))*1.5f) / (Y_NUM));
			}
		}
		const int RECT_NUM = (X_NUM - 1) * (Y_NUM - 1);
		var triangles = new int[RECT_NUM * 2 * 3];
		int i = 0;
		for (int y = 0; y < Y_NUM-1; ++y) {
			for (int x = 0; x < X_NUM-1; ++x) {
				triangles[i] = (y + 0) * (X_NUM) + x + 0; ++i;
				triangles[i] = (y + 1) * (X_NUM) + x + 0; ++i;
				triangles[i] = (y + 0) * (X_NUM) + x + 1; ++i;
				triangles[i] = (y + 1) * (X_NUM) + x + 0; ++i;
				triangles[i] = (y + 1) * (X_NUM) + x + 1; ++i;
				triangles[i] = (y + 0) * (X_NUM) + x + 1; ++i;
			}
		}

		Mesh mesh = new Mesh();
		mesh.name = "PaperMesh";
		mesh.vertices = vertices;
		mesh.uv = uvs;
		mesh.triangles = triangles;		
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		GetComponent<MeshFilter>().sharedMesh = mesh;
	}
}
