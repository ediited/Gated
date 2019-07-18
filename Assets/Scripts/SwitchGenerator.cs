using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGenerator : MonoBehaviour
{
    public int GatesMaxDepth;
    public bool placeRandomly = true;
    public bool startSendingEnergy = false;
    public GameObject NOTPrefab;
    public GameObject SWITCHPrefab;
    public GameObject SPLITPrefab;
    public GameObject ANDPrefab;
    public GameObject NANDPrefab;
    public GameObject ORPrefab;
    public GameObject NORPrefab;
    public GameObject XORPrefab;
    public GameObject XNORPrefab;
    public GameObject Particles;
    public GameObject ONPrefab;
    public GameObject OffPrefab;
    public GameObject GateCollector;

    private List<Gates> allGates = new List<Gates>();
    private int cD = 0;
    // Start is called before the first frame update
    private List<Cable> dontGCtheCables = new List<Cable>();
    void Start()
    {

        //warum auch immer tf das nötig ist 

        //TestSetup();
        generate();

    }
    void generate()
    {
        //find the deepest left sided Tree branch
        //während der aktuelle Gate noch Inputs hat gehts immer tiefer,
        //sollte der Leftmost Input noch NULL sein wird er gesetzt
        //Check Input 1
        //if null -> create  
        //pointer = create
        //else
        //check Input 2
        //if null -> create
        //else 
        //pointer = pointer.parent
        //if pointer.parent == null break; 


        Gates startGate;
        Gates GatePointer;
        GameObject GOPointer;
        GOPointer = Instantiate(initiateRandomGate()); //creates the first / the "end" Gate
        GatePointer = GOPointer.GetComponent<Gates>();
        GameObject toConnect;
        startGate = GatePointer;

        cD = 0;
        int breakAfter = 200;
        while (true)
        {

            //Debug.Log(GOPointer.GetComponent<Gates>().inputCount) ;
            //if (breakAfter <= 0)
            //{
            //    Debug.Log("break due to too deep search");
            //    break;

            //}
            //breakAfter--;


            while (true)
            {
                //returning once lowest level is reached;
                //or an END Gate
                if (GOPointer.GetComponent<Gates>().inputCount == 0 || (cD > GatesMaxDepth))
                {
                    //start ist das untere Ende daher muss dieses gefunden werden -> Output - Start -> Input -> End
                    GOPointer = GOPointer.GetComponent<Gates>().Output1.connection.start.parent.gameObject;
                    cD--;
                    break;
                }
                //if left input is empty create a new one and break;
                //then go one layer deeper;
                //20% chance for it to be an early end Gate;
                if ((GOPointer.GetComponent<Gates>().Input1 == null) && GOPointer.GetComponent<Gates>().inputCount > 0)
                {
                    if (Random.Range(0, 100) > 10 && cD < GatesMaxDepth)
                    {
                        //instantiate the new Object and Place it accordingly to its depth in the Tree
                        toConnect = Instantiate(initiateRandomGate(), new Vector3(cD * 10, 0, Random.Range(0, 80)), new Quaternion(0f, 0f, 0f, 0f), GateCollector.transform);
                        //add the new Gate to the List of Gates
                        allGates.Add(toConnect.GetComponent<Gates>());
                        //Add the Port to the Pointer
                        GOPointer.GetComponent<Gates>().Input1 = new Ports(false, GOPointer.GetComponent<Gates>().TInput1, GOPointer.GetComponent<Gates>()); //create All the necessary Ports
                        //Add the Port to the "toConnect" 
                        toConnect.GetComponent<Gates>().Output1 = new Ports(false, toConnect.GetComponent<Gates>().TOutput1, toConnect.GetComponent<Gates>());
                        //creating a Cable automatically connects it
                        //adding it to a List to ensure the Garbage Collector doesnt get to it
                        toConnect.GetComponent<Gates>().Output1.connection = new Cable(GOPointer.GetComponent<Gates>().Input1, toConnect.GetComponent<Gates>().Output1);
                        dontGCtheCables.Add(toConnect.GetComponent<Gates>().Output1.connection);

                        GOPointer = toConnect;
                        cD++;
                    }
                    else 
                    {
                        //instantiate the new Object and Place it accordingly to its depth in the Tree
                        toConnect = Instantiate(initiateRandomEndGate(), new Vector3(cD * 10, 0, Random.Range(0f, 80f)), new Quaternion(0f, 0f, 0f, 0f), GateCollector.transform);
                        //add the new Gate to the List of Gates
                        allGates.Add(toConnect.GetComponent<Gates>());
                        //Add the Port to the Pointer
                        GOPointer.GetComponent<Gates>().Input1 = new Ports(false, GOPointer.GetComponent<Gates>().TInput1, GOPointer.GetComponent<Gates>()); //create All the necessary Ports
                        //Add the Port to the "toConnect" 
                        toConnect.GetComponent<Gates>().Output1 = new Ports(false, toConnect.GetComponent<Gates>().TOutput1, toConnect.GetComponent<Gates>());
                        //creating a Cable automatically connects it
                        //adding it to a List to ensure the Garbage Collector doesnt get to it
                        toConnect.GetComponent<Gates>().Output1.connection = new Cable(GOPointer.GetComponent<Gates>().Input1, toConnect.GetComponent<Gates>().Output1);
                        dontGCtheCables.Add(toConnect.GetComponent<Gates>().Output1.connection);
                        
                        GOPointer = toConnect;
                        cD++;
                    }
                    break;
                }
                //if the left one isnt empty check the right one
                //if empty create a new one
                //and go deeper
                if (GOPointer.GetComponent<Gates>().inputCount > 1)
                {
                    if (GOPointer.GetComponent<Gates>().Input2 == null)
                    {
                        if (Random.Range(0, 100) > 10 && cD < GatesMaxDepth)
                        {
                            //instantiate the new Object and Place it accordingly to its depth in the Tree
                            toConnect = Instantiate(initiateRandomGate(), new Vector3(cD * 10, 0, Random.Range(0, 80)), new Quaternion(0f, 0f, 0f, 0f), GateCollector.transform);
                            //add the new Gate to the List of Gates
                            allGates.Add(toConnect.GetComponent<Gates>());
                            //Add the Port to the Pointer
                            GOPointer.GetComponent<Gates>().Input2 = new Ports(false, GOPointer.GetComponent<Gates>().TInput2, GOPointer.GetComponent<Gates>()); //create All the necessary Ports
                                                                                                                                                                 //Add the Port to the "toConnect" 
                            toConnect.GetComponent<Gates>().Output1 = new Ports(false, toConnect.GetComponent<Gates>().TOutput1, toConnect.GetComponent<Gates>());
                            //creating a Cable automatically connects it
                            //adding it to a List to ensure the Garbage Collector doesnt get to it
                            toConnect.GetComponent<Gates>().Output1.connection = new Cable(GOPointer.GetComponent<Gates>().Input2, toConnect.GetComponent<Gates>().Output1);
                            dontGCtheCables.Add(toConnect.GetComponent<Gates>().Output1.connection);

                            GOPointer = toConnect;
                            cD++;
                        }
                        else
                        {
                            //instantiate the new Object and Place it accordingly to its depth in the Tree
                            toConnect = Instantiate(initiateRandomEndGate(), new Vector3(cD * 10, 0, Random.Range(0, 80)), new Quaternion(0f, 0f, 0f, 0f), GateCollector.transform);
                            //add the new Gate to the List of Gates
                            allGates.Add(toConnect.GetComponent<Gates>());
                            //Add the Port to the Pointer
                            GOPointer.GetComponent<Gates>().Input2 = new Ports(false, GOPointer.GetComponent<Gates>().TInput2, GOPointer.GetComponent<Gates>()); //create All the necessary Ports
                                                                                                                                                                 //Add the Port to the "toConnect" 
                            toConnect.GetComponent<Gates>().Output1 = new Ports(false, toConnect.GetComponent<Gates>().TOutput1, toConnect.GetComponent<Gates>());
                            //creating a Cable automatically connects it
                            //adding it to a List to ensure the Garbage Collector doesnt get to it
                            toConnect.GetComponent<Gates>().Output1.connection = new Cable(GOPointer.GetComponent<Gates>().Input2, toConnect.GetComponent<Gates>().Output1);
                            dontGCtheCables.Add(toConnect.GetComponent<Gates>().Output1.connection);

                            GOPointer = toConnect;
                            cD++;
                        }
                        break;

                    }
                }

                //if neither is the case go up one layer
                //and repeat the process
                if (GOPointer.GetComponent<Gates>().Output1 != null)
                {
                    GOPointer = GOPointer.GetComponent<Gates>().Output1.connection.start.parent.gameObject;
                    cD--;
                    //Debug.Log(GatePointer);
                    break;
                }

                if (GOPointer.GetComponent<Gates>().Output1 == null)
                {
                    //should only happen once weve reached the entire tree
                    //break both loops and the function
                    return;
                }
                break;
            }

        }
    }

    GameObject getRandomGate()
    {

        switch (Mathf.FloorToInt(Random.Range(0, 7)))
        {
            case 0:
                return NOTPrefab;
                break;
            case 1:
                return ANDPrefab;
                break;
            case 2:
                return NANDPrefab;
                break;
            case 3:
                return ORPrefab;
                break;
            case 4:
                return NORPrefab;
                break;
            case 5:
                return XORPrefab;
                break;
            case 6:
                return XNORPrefab;
                break;
                /*
                case 7: return SPLITPrefab;
                    break;
                    */
        }


        return null;
    }
    GameObject initiateRandomGate()
    {
        GameObject toReturn = getRandomGate();
        return toReturn;
    }
    GameObject initiateRandomEndGate()
    {
        GameObject toReturn = getRandomEndGate();
        return toReturn;
    }
    GameObject getRandomEndGate()
    {
        switch (Mathf.FloorToInt(Random.Range(0, 3)))
        {
            case 0:
                return SWITCHPrefab;
                break;
            case 1:
                return ONPrefab;
                break;
            case 2:
                return OffPrefab;
                break;
        }
        return null;
    }

    void TestSetup()
    {

        GameObject temp2;
        GameObject temp1;

        temp1 = Instantiate(SWITCHPrefab);
        temp2 = Instantiate(NOTPrefab);

        Gates temp1C = temp1.GetComponent<Gates>();

        //connecting two Parts
        //you only need to connect from->to since the rest is done in the Cable class

        //temp1.GetComponent<Gates>().Output1 = new Ports(false, temp1.GetComponent<Gates>().TOutput1, temp1.GetComponent<Gates>());
        temp1C.Output1 = new Ports(false, temp1.GetComponent<Gates>().TOutput1, temp1.GetComponent<Gates>());

        temp2.GetComponent<Gates>().Input1 = new Ports(false, temp2.GetComponent<Gates>().TInput1, temp2.GetComponent<Gates>());

        //Cable doYouEvenExist = new Cable(temp1.GetComponent<Gates>().Output1, temp2.GetComponent<Gates>().Input1);

        //Ports whyCantIConstruct = new Ports(false, temp1.GetComponent<Gates>().TOutput1, temp1.GetComponent<Gates>());

        temp1C.Output1.connection = new Cable(temp1.GetComponent<Gates>().Output1, temp2.GetComponent<Gates>().Input1);

    }


    void connectPorts(Ports p1, Ports p2)
    {
        dontGCtheCables.Add(new Cable(p1, p2));
    }

    // Update is called once per frame
    void Update()
    {
        if (startSendingEnergy)
        {
            Debug.Log("Gates"+allGates.Count);
            Debug.Log("Cables" + dontGCtheCables.Count);
            for (var i = 0; i < allGates.Count; i++)
            {
                allGates[i].SendEnergy();
            }
            for(var i = 0; i < dontGCtheCables.Count; i++)
            {
                dontGCtheCables[i].colorTheCable();
                if (dontGCtheCables[i].start.state) {

                    dontGCtheCables[i].LineRendererContainer.GetComponent<LineRenderer>().SetColors(Color.green, Color.green);                }
                else
                {
                   
                }
            }
        }
    }
}
/* while (true)
            {
                //returning once lowest level is reached;
                //or an END Gate
                if (GatePointer.inputCount == 0 || (cD == GatesMaxDepth))
                {
                    //start ist das untere Ende daher muss dieses gefunden werden -> Output - Start -> Input -> End
                    GatePointer = GatePointer.Output1.connection.start.parent;
                    cD--;
                    break;
                }
                //if left input is empty create a new one and break;
                //then go one layer deeper;
                //20% chance for it to be an early end Gate;
                if ((GatePointer.Input1 == null) && GatePointer.inputCount > 0)
                {
                    if (Random.Range(0, 100) > 5 && cD <= GatesMaxDepth)
                    {
                        //instantiate the new Object and Place it accordingly to its depth in the Tree
                        toConnect = Instantiate(initiateRandomGate(), new Vector3(cD * 20, 0, Random.Range(0, 40)), new Quaternion(0f, 0f, 0f, 0f), GateCollector.transform);
                        //add the new Gate to the List of Gates
                        allGates.Add(toConnect.GetComponent<Gates>());
                        //Add the Port to the Pointer
                        GatePointer.Input1 = new Ports(false, GatePointer.TInput1, GatePointer); //create All the necessary Ports
                        //Add the Port to the "toConnect" 
                        toConnect.GetComponent<Gates>().Output1 = new Ports(false, toConnect.GetComponent<Gates>().TOutput1, toConnect.GetComponent<Gates>());
                        //creating a Cable automatically connects it
                        //adding it to a List to ensure the Garbage Collector doesnt get to it
                        toConnect.GetComponent<Gates>().Output1.connection = new Cable(GatePointer.Input1, toConnect.GetComponent<Gates>().Output1);
                        dontGCtheCables.Add(toConnect.GetComponent<Gates>().Output1.connection);

                        GatePointer = toConnect.GetComponent<Gates>();
                        cD++;
                    }
                    else
                    {
                        //instantiate the new Object and Place it accordingly to its depth in the Tree
                        toConnect = Instantiate(initiateRandomEndGate(), new Vector3(cD * 20, 0, Random.Range(0, 40)), new Quaternion(0f, 0f, 0f, 0f), GateCollector.transform);
                        //add the new Gate to the List of Gates
                        allGates.Add(toConnect.GetComponent<Gates>());
                        //Add the Port to the Pointer
                        GatePointer.Input1 = new Ports(false, GatePointer.TInput1, GatePointer); //create All the necessary Ports
                        //Add the Port to the "toConnect" 
                        toConnect.GetComponent<Gates>().Output1 = new Ports(false, toConnect.GetComponent<Gates>().TOutput1, toConnect.GetComponent<Gates>());
                        //creating a Cable automatically connects it
                        //adding it to a List to ensure the Garbage Collector doesnt get to it
                        toConnect.GetComponent<Gates>().Output1.connection = new Cable(GatePointer.Input1, toConnect.GetComponent<Gates>().Output1);
                        dontGCtheCables.Add(toConnect.GetComponent<Gates>().Output1.connection);

                        GatePointer = toConnect.GetComponent<Gates>();
                        cD++;
                    }
                    break;
                }
                //if the left one isnt empty check the right one
                //if empty create a new one
                //and go deeper
                if (GatePointer.inputCount > 1)
                {
                    if (GatePointer.Input2 == null)
                    {
                        if (Random.Range(0, 100) > 5 && cD < GatesMaxDepth)
                        {
                            //instantiate the new Object and Place it accordingly to its depth in the Tree
                            toConnect = Instantiate(initiateRandomGate(), new Vector3(cD * 20, 0, Random.Range(0, 40)), new Quaternion(0f, 0f, 0f, 0f), GateCollector.transform);
                            //add the new Gate to the List of Gates
                            allGates.Add(toConnect.GetComponent<Gates>());
                            //Add the Port to the Pointer
                            GatePointer.Input2 = new Ports(false, GatePointer.TInput2, GatePointer); //create All the necessary Ports
                                                                                                     //Add the Port to the "toConnect" 
                            toConnect.GetComponent<Gates>().Output1 = new Ports(false, toConnect.GetComponent<Gates>().TOutput1, toConnect.GetComponent<Gates>());
                            //creating a Cable automatically connects it
                            //adding it to a List to ensure the Garbage Collector doesnt get to it
                            toConnect.GetComponent<Gates>().Output1.connection = new Cable(GatePointer.Input2, toConnect.GetComponent<Gates>().Output1);
                            dontGCtheCables.Add(toConnect.GetComponent<Gates>().Output1.connection);

                            GatePointer = toConnect.GetComponent<Gates>();
                            cD++;
                        }
                        else
                        {
                            //instantiate the new Object and Place it accordingly to its depth in the Tree
                            toConnect = Instantiate(initiateRandomEndGate(), new Vector3(cD * 20, 0, Random.Range(0, 40)), new Quaternion(0f, 0f, 0f, 0f), GateCollector.transform);
                            //add the new Gate to the List of Gates
                            allGates.Add(toConnect.GetComponent<Gates>());
                            //Add the Port to the Pointer
                            GatePointer.Input2 = new Ports(false, GatePointer.TInput2, GatePointer); //create All the necessary Ports
                                                                                                     //Add the Port to the "toConnect" 
                            toConnect.GetComponent<Gates>().Output1 = new Ports(false, toConnect.GetComponent<Gates>().TOutput1, toConnect.GetComponent<Gates>());
                            //creating a Cable automatically connects it
                            //adding it to a List to ensure the Garbage Collector doesnt get to it
                            toConnect.GetComponent<Gates>().Output1.connection = new Cable(GatePointer.Input2, toConnect.GetComponent<Gates>().Output1);
                            dontGCtheCables.Add(toConnect.GetComponent<Gates>().Output1.connection);

                            GatePointer = toConnect.GetComponent<Gates>();
                            cD++;
                        }
                        break;

                    }
                }

                //if neither is the case go up one layer
                //and repeat the process
                if (GatePointer.Output1 != null)
                {
                    GatePointer = GatePointer.Output1.connection.start.parent;
                    cD--;
                    Debug.Log(GatePointer);
                    break;
                }

                if (GatePointer.Output1 == null)
                {
                    //should only happen once weve reached the entire tree
                    //break both loops and the function
                    return;
                }
                break;
            }

        }
*/