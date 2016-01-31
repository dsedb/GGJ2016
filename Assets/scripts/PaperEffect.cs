using UnityEngine;
using System.Collections;

[RequireComponent (typeof (MeshFilter))]
[RequireComponent (typeof (MeshRenderer))]
public class PaperEffect : MonoBehaviour
{
	[Range(-1, 1)]
	public float s = 0f;
	[Range(-1, 1)]
	public float t = 0f;
	private const float width = 0.667f;
	private const float height = 1.5f;
	private const int X_NUM = 16;
	private const int Y_NUM = 32;
	private float prev_s = 0f;

	public void setValue(float v)
	{
		s = Mathf.Clamp(v, -1f, 1f);
		t = 0f;
	}

	public void setTwistValue(float v)
	{
		s = 0f;
		t = Mathf.Clamp(v, -1f, 1f);
	}

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
				uvs [x + y * X_NUM] = new Vector2 (((float)x) / X_NUM, ((float)y) / Y_NUM);
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
		GetComponent<MeshRenderer>().enabled = false; // not to show for the first frame.
	}

	private void update_twist()
	{
		float l = height;
		float rl = 1.0f/l;
		float V = 2f;

		float theta = (t * Mathf.PI * 0.05f);
		float p0orgx = -width*0.5f;
		float p0orgy = height*0.5f;
		float p0x = p0orgx * Mathf.Cos(theta) - p0orgy * Mathf.Sin(theta);
		float p0y = p0orgx * Mathf.Sin(theta) + p0orgy * Mathf.Cos(theta);
		float p1orgx = width*0.5f;
		float p1orgy = height*0.5f;
		float p1x = p1orgx * Mathf.Cos(theta) - p1orgy * Mathf.Sin(theta);
		float p1y = p1orgx * Mathf.Sin(theta) + p1orgy * Mathf.Cos(theta);
		float p2x = -width*0.5f;
		float p2y = 0f;
		float p3x = width*0.5f;
		float p3y = 0f;
			
		var vertices = new Vector3[X_NUM * Y_NUM];
		for (int y = 0; y < Y_NUM; ++y) {
			for (int x = 0; x < X_NUM; ++x) {
				float xi = (x*width/X_NUM) - width*0.5f;
				float yi = (y*height/Y_NUM) - height*0.5f;
				float z = 0f;
				if (yi < 0f) {
					z = -2 * yi * xi * V * t * rl;
					vertices[x + y * X_NUM] = new Vector3(xi, yi, z);
				} else {
					float paramT = xi/width + 0.5f;
					float paramS = 1f - yi/(height*0.5f);
					float q0x = p0x*(1f-paramT) + paramT*p1x;
					float q0y = p0y*(1f-paramT) + paramT*p1y;
					float q1x = p2x*(1f-paramT) + paramT*p3x;
					float q1y = p2y*(1f-paramT) + paramT*p3y;
					float qx = q0x*(1f-paramS) + paramS*q1x;
					float qy = q0y*(1f-paramS) + paramS*q1y;
					vertices[x + y * X_NUM] = new Vector3(qx, qy, z);
				}
				// vertices[x + y * X_NUM] = new Vector3(xi, yi, z);
			}
		}
		GetComponent<MeshFilter>().sharedMesh.vertices = vertices;
	}

	void Update()
	{
		GetComponent<MeshRenderer>().enabled = true;
		if (t != 0f) {
			update_twist();
			return;
		}
		if (prev_s == s) {		// suppress recalcuration
			return;
		}
		prev_s = s;

		const float R0 = 0.08f;
		const float R1 = 0.09f;
		float l = height;
		float rl = 1.0f/l;
		var vertices = new Vector3[X_NUM * Y_NUM];
		for (int y = 0; y < Y_NUM; ++y) {
			for (int x = 0; x < X_NUM; ++x) {
				float xi = (x*width/X_NUM) - width*0.5f;
				float yi = (y*height/Y_NUM) - height*0.5f;
				if (0 < s) {
					float p = l * s;
					float k = yi - (l*0.5f - p);
					if (k <= 0) {
						vertices[x + y * X_NUM] = new Vector3(xi, yi, 0f);
					} else {
						float r = (1f - (k*rl)) * R0 + (k*rl)*R1;
						float yn = (l*0.5f - p) + r * (float)System.Math.Sin(k/r);
						float zn = R0 - r * (float)System.Math.Cos(k/r);
						vertices[x + y * X_NUM] = new Vector3(xi, yn, zn);
					}
				} else if (s < 0) {
					float p = -l * s;
					float k = -(l*0.5f) + p - yi;
					if (k <= 0) {
						vertices[x + y * X_NUM] = new Vector3(xi, yi, 0f);
					} else {
						float r = (1f - (k*rl)) * R0 + (k*rl)*R1;
						float yn = (-l*0.5f + p) + r * (float)System.Math.Sin(k/r);
						float zn = R0 - r * (float)System.Math.Cos(k/r);
						vertices[x + y * X_NUM] = new Vector3(xi, yn, zn);
					}
				} else {
					vertices[x + y * X_NUM] = new Vector3(xi, yi, 0f);
				}
			}
		}
		GetComponent<MeshFilter>().sharedMesh.vertices = vertices;
	}

}
