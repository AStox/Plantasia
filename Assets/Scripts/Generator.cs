using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    // MeshFilter mf;
    Mesh mesh;
    Deform.Deformable deformer;
    bool dirty = false;
    public float radius = 1;
    
    public int numberOfVertices = 4;
    
    public float height = 4;
    
    public int numberOfRings = 2;
    public AnimationCurve rCurve;
    float newRadius = -1;
    int newNumberOfVertices = -1;
    float newHeight = -1;
    int newNumberOfRings = -1;

    void Start()
    {
        mesh = new Mesh();
        mesh.name = "StalkMesh";
        GetComponent<MeshFilter>().sharedMesh = mesh;
        deformer = GetComponent<Deform.Deformable>();
        setMesh();
        GetComponent<Deform.Deformable>().InitializeData();
        GetComponent<Deform.Deformable>().ChangeMesh(mesh);

    }

    // Update is called once per frame
    void Update()
    {
        // GetComponent<Deform.Deformable>().Complete();
        if (Input.GetKey("q")){
            numberOfVertices += 1;
        }
        if (Input.GetKey("a")){
            numberOfVertices -= 1;
        }
        if (Input.GetKey("w")){
            radius += 1;
        }
        if (Input.GetKey("s")){
            radius -= 1;
        }
        if (Input.GetKey("e")){
            numberOfRings += 1;
        }
        if (Input.GetKey("d")){
            numberOfRings -= 1;
        }
        if (Input.GetKey("r")){
            // height += 1;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z + 0.01f);
        }
        if (Input.GetKey("f")){
            // height -= 1;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z - 0.01f);
        }
        // CheckDirty();
        if (dirty) {
            setMesh();
            
            dirty = false;
        }
        // setMesh();
    }

    List<Vector3> PointsOnCircle(int vn, int rn, float r, Vector3 tip) {
        List<Vector3> res = new List<Vector3>();
        for(int i = 0; i < rn; i++){
            Vector3 center = tip + Vector3.forward * ((float)i/rn) * height;
            for (int j = 0; j < vn; j++) {
                Vector3 vec = new Vector3(center.x + r * Mathf.Cos(6.28f*((float)j/vn)), center.y + r * Mathf.Sin(6.28f*((float)j/vn)), center.z);
                // vec = transform.TransformPoint(vec);
                res.Add(vec);
            }
        }
        return res;
    }

    List<int> CircleTris(int vn, int rn, float r, Vector3 center) {
        if(vn >= 3) {
            List<int> res = new List<int>();
            for(int i = 0; i < rn; i++){ //Makes the tip faces
                if (i == 0) {
                    for (int j = 0; j < (vn - 2); j++) {
                        res.Add(0);
                        res.Add(j+2);
                        res.Add(j+1);
                    }
                } else { //Makes the shaft faces
                    for (int j = 0; j < vn; j++) {
                        if (j == vn - 1) {
                            res.Add(((i - 1)*vn) + j);
                            res.Add(((i - 1)*vn));
                            res.Add((i*vn));
                            res.Add(((i - 1)*vn) + j);
                            res.Add((i*vn));
                            res.Add((i*vn) + j);
                        } else {
                            res.Add(((i - 1)*vn) + j);
                            res.Add(((i - 1)*vn) + j + 1);
                            res.Add((i*vn) + j + 1);
                            res.Add(((i - 1)*vn) + j);
                            res.Add((i*vn) + j + 1);
                            res.Add((i*vn) + j);
                        }

                    }
                }
            }
            return res;
        }
        return new List<int>();
    }

    List<Vector3> CircleNormals(int vn, int rn) {
        List<Vector3> res = new List<Vector3>();
        for (int i = 0; i < vn * rn; i++) {
            res.Add(transform.forward);
        }
        return res;
    }

    List<Vector2> CircleUV(int vn, int rn) {
        List<Vector2> res = new List<Vector2>();
        for (int i = 0; i < rn; i++) {
            for (int j = 0; j < vn; j++) {
                res.Add(new Vector3(Mathf.Cos(6.28f*((float)j/vn)), Mathf.Sin(6.28f*((float)j/vn))));
            }
        }
        return res;
    }

    void setMesh() {
        // mesh = new Mesh();
        // mesh.name = "StalkMesh";
        // if (!GetComponent<MeshFilter>().sharedMesh){
        // }
        List<Vector3> vertices = PointsOnCircle(numberOfVertices, numberOfRings, radius, Vector3.zero);
        List<int> tris = CircleTris(numberOfVertices, numberOfRings, radius, Vector3.zero);
        List<Vector3> normals = CircleNormals(numberOfVertices, numberOfRings);
        List<Vector2> uv = CircleUV(numberOfVertices,  numberOfRings);

        Vector3[] verticesArray = vertices.ToArray();
        int[] trisArray = tris.ToArray();
        Vector2[] uvArray = uv.ToArray();
        Vector3[] normalsArray = normals.ToArray();

        mesh.vertices = verticesArray;
        mesh.triangles = trisArray;
        mesh.uv = uvArray;
        mesh.normals = normalsArray;
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        // GetComponent<Deform.Deformable>().Complete();
        // // GetComponent<Deform.Deformable>().ChangeMesh(mesh);
        // GetComponent<Deform.Deformable>().Complete();
        // GetComponent<Deform.Deformable>().ApplyData();
        // GetComponent<Deform.Deformable>().Complete();
        GetComponent<MeshFilter>().sharedMesh = mesh;
    }

    public string ToString<T>(List<T> list) {
        string temp = "";
        if(list != null){
            if (list is List<Vector3>){
                for(int i = 0; i < list.Count; i++){
                    temp += list[i].ToString() + ", ";
                }
            } else if (list is List<int>) {
                for(int i = 0; i < list.Count; i++){
                    temp += list[i] + ", ";
                }
            } else if (list is List<Vector2>) {
                for(int i = 0; i < list.Count; i++){
                    temp += list[i].ToString() + ", ";
                }
            }
        }
        return temp;
    }

    private void CheckDirty(){

        if (newRadius != radius) {
            dirty = true;
            newRadius = radius;
        }
        if (newNumberOfVertices != numberOfVertices) {
            dirty = true;
            newNumberOfVertices = numberOfVertices;
        }
        if (newHeight != height) {
            dirty = true;
            newHeight = height;
        }
        if (newNumberOfRings != numberOfRings) {
            dirty = true;
            newNumberOfRings = numberOfRings;
        }
    }
}