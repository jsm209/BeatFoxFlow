using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Composer : MonoBehaviour {

    // The repetoire of this composer.
    Dictionary<string, ArrayList> repetoire = new Dictionary<string, ArrayList>();

    // The melodic progressions for danger level 1.
    ArrayList danger1Melodies = new ArrayList();

    // The melodic progressions for danger level 1.
    ArrayList danger1Chords = new ArrayList();


    void Awake () {
        /*
        danger1.Add (new int[4, 3] {
            {2, 4, 6},
            {5, 7, 2},
            {1, 3, 5},
            {6, 1, 3}
        });
        */


        danger1Melodies.Add(new int[][] {
            new int[] {1},
            new int[] {3},
            new int[] {5},
            new int[] {7},
            new int[] {1},
            new int[] {3},
            new int[] {7},
            new int[] {5},
        });

        danger1Melodies.Add(new int[][] {
            new int[] {1},
            new int[] {2},
            new int[] {1},
            new int[] {2},
            new int[] {1},
            new int[] {2},
            new int[] {1},
            new int[] {2},
        });

        danger1Melodies.Add(new int[][] {
            new int[] {7},
            new int[] {5},
            new int[] {6},
            new int[] {4},
            new int[] {5},
            new int[] {3},
            new int[] {4},
            new int[] {2},
        });

        danger1Melodies.Add(new int[][] {
            new int[] {7},
            new int[] {0},
            new int[] {6},
            new int[] {0},
            new int[] {5},
            new int[] {0},
            new int[] {3},
            new int[] {0},
        });

        danger1Melodies.Add(new int[][] {
            new int[] {2},
            new int[] {3},
            new int[] {4},
            new int[] {5},
            new int[] {4},
            new int[] {3},
            new int[] {2},
            new int[] {0},
        });

        danger1Melodies.Add(new int[][] {
            new int[] {2},
            new int[] {0},
            new int[] {5},
            new int[] {0},
            new int[] {1},
            new int[] {0},
            new int[] {1},
            new int[] {0},
        });

        danger1Chords.Add(new int[][] {
            new int[] {4, 6, 1},
            new int[] {4, 6, 1},
            new int[] {4, 6, 1},
            new int[] {1, 3, 5},
            new int[] {1, 3, 5},
            new int[] {1, 3, 5},
            new int[] {1, 3, 5},
            new int[] {1, 3, 5},
        });

        danger1Chords.Add(new int[][] {
            new int[] {2, 4, 6},
            new int[] {2, 4, 6},
            new int[] {2, 4, 6},
            new int[] {2, 4, 6},
            new int[] {3, 5, 6},
            new int[] {3, 5, 6},
            new int[] {3, 5, 6},
            new int[] {3, 5, 6},
        });

        danger1Chords.Add(new int[][] {
            new int[] {2, 4, 6},
            new int[] {2, 4, 6},
            new int[] {5, 7, 2},
            new int[] {5, 7, 2},
            new int[] {1, 3, 5},
            new int[] {1, 3, 5},
            new int[] {1, 3, 5},
            new int[] {1, 3, 5},
        });

        repetoire.Add("danger1Melodies", danger1Melodies);
        repetoire.Add("danger1Chords", danger1Chords);
    }

    public int[][] getProgression() {

        if (gameObject.tag == "melody") {
            // we want to return the arraylist at the map key 1 (danger level 1)
            ArrayList r = repetoire["danger1Melodies"];
            return (int[][])r[Random.Range(0, r.Count)];
        } else {
            ArrayList r = repetoire["danger1Chords"];
            return (int[][])r[Random.Range(0, r.Count)];
        }
        
    }
	
}
