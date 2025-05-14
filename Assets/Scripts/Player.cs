using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Player : Item
{
    public static Player Instance;

    public TileData heldTile;

    private Vector2 playerDirection;
    public Transform sprite;
    public GameObject questionReaction;
    public GameObject exclamationReaction;
    public Image heldItemUI;
    public TMP_Text potionCountUI;
    public TMP_Text stageNumberUI;
    private Animator anim;


    private PlayerInput controls;
    private InputAction movementInputAction;
    private InputAction interactInputAction;

    private Vector2 movementInputDirection;
    private bool interactInput;

    public float hangTime = 1;
    private Vector2 hangReturnDirection;
    private bool hanging;

    public float inputSpacing = 0.1f;
    public float kickTime = 0.05f;
    public float kickDistance = 1.8f;
    public float reactionTime = 0.8f;
    public float updateDelay = 0.01f;
    private float inputDelay = 0;

    public int potionCount = 0;


    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);

        controls = GetComponent<PlayerInput>();
        controls.onActionTriggered += ReadInputAction;
        movementInputAction = controls.currentActionMap.FindAction("Movement");
        interactInputAction = controls.currentActionMap.FindAction("Interact");

        anim = sprite.GetComponent<Animator>();
    }

    private void Start()
    {
        playerDirection = new Vector2 (0, -1);
    }



    void Update()
    {
        if (inputDelay > 0) inputDelay -= Time.deltaTime;


        if (inputDelay <= 0)
        {
            if (movementInputDirection != Vector2.zero)
            {
                Node n = null;
                if (movementInputDirection == new Vector2(0, 1)) n = node.top;
                if (movementInputDirection == new Vector2(0, -1)) n = node.bottom;
                if (movementInputDirection == new Vector2(-1, 0)) n = node.left;
                if (movementInputDirection == new Vector2(1, 0)) n = node.right;

                if (n == null)
                {
                    n = new Node();
                    n.tileType = TileTypes.Wall;
                }
                    
                StartCoroutine(MoveToNode(n, movementInputDirection));
            }
        }

        if (inputDelay <= 0)
        {
            if (interactInput)
            {
                Node n = null;
                if (playerDirection == new Vector2(0, 1)) n = node.top;
                if (playerDirection == new Vector2(0, -1)) n = node.bottom;
                if (playerDirection == new Vector2(-1, 0)) n = node.left;
                if (playerDirection == new Vector2(1, 0)) n = node.right;

                if (n == null)
                {
                    n = new Node();
                    n.tileType = TileTypes.Wall;
                }
                
                StartCoroutine(InteractWithNode(n, playerDirection));
            }
        }
    }


    public void ReadInputAction(InputAction.CallbackContext context)
    {
        // Movement
        if (context.action == movementInputAction)
        {
            if (context.performed)
            {
                // Remove diagonal inputs
                Vector2 i = context.ReadValue<Vector2>();
                if (i.x != 0 && i.y != 0)
                {
                    if (Mathf.Abs(i.x) > Mathf.Abs(i.y)) i.y = 0;
                    else i.x = 0;
                }

                i.Normalize();
                movementInputDirection = i;
            }

            if (context.canceled) movementInputDirection = Vector2.zero;
        }


        // Interact
        if (context.action == interactInputAction)
        {
            if (context.started) interactInput = true;
            if (context.canceled) interactInput = false;
        }
    }


    public IEnumerator MoveToNode(Node n, Vector2 d)
    {
        if (hanging)
        {
            if (hangReturnDirection != d)
            {
                StartCoroutine(KillPlayer());
                yield break;
            }
            else
            {
                hanging = false;
                hangReturnDirection = Vector2.zero;
                StopCoroutine("WaterHang");
            }
        }


        playerDirection = d;
        SpriteSwap();
        Debug.Log(d + " - " + n.tileType + " - " + n.itemType);
        bool valid = true;
        if (n.tileType == TileTypes.Wall) valid = false;
        if (n.itemType != ItemTypes.None) valid = false;


        if (valid == false) StartCoroutine(Kick(n, d));
        else
        {
            node.itemType = ItemTypes.None;
            node.itemObject = null;
            node.item = null;

            transform.position = n.transform.position;
            transform.parent = n.transform;
            node = n;

            node.itemType = type;
            node.itemObject = gameObject;
            node.item = this;

            if (node.tileType == TileTypes.Null) 
                StartCoroutine(WaterHang(d));
        }



        movementInputDirection = Vector2.zero;
        interactInput = false;
        inputDelay = inputSpacing;
        exclamationReaction.SetActive(false);
        questionReaction.SetActive(false);

        yield return new WaitForSeconds(updateDelay);

        GridManager.Instance.UpdateGrid();
    }    

    public IEnumerator Kick(Node n, Vector2 d)
    {
        Vector3 startPos = sprite.position;
        sprite.position = startPos + new Vector3(d.x / kickDistance, d.y / kickDistance, 0);

        yield return new WaitForSeconds(kickTime);

        Vector2 sideKicked = new Vector2(-d.x, -d.y);
        if (n.tile != null) n.tile.Kick(sideKicked);
        if (n.item != null) n.item.Kick(sideKicked);

        sprite.position = startPos;
    }


    public IEnumerator InteractWithNode(Node n, Vector2 d)
    {
        Vector2 sideInteracted = new Vector2(-d.x, -d.y);
        n.Interact(sideInteracted);

        movementInputDirection = Vector2.zero;
        interactInput = false;
        inputDelay = inputSpacing;

        yield return new WaitForSeconds(updateDelay);

        GridManager.Instance.UpdateGrid();
    }


    public void PickUpTile(Tile t)
    {
        heldTile = t.data;
        heldItemUI.sprite = t.data.sprite;
        heldItemUI.color = Color.white;

        t.node.tile = null;
        t.node.tileObject = null;
        t.node.tileType = TileTypes.Null;

        Destroy(t.gameObject);
    }

    public void PlaceTile(Node n)
    {
        n.tileObject = GameObject.Instantiate(heldTile.prefab, n.transform.position, n.transform.rotation, n.transform);
        n.tile = n.tileObject.transform.GetComponent<Tile>();
        n.tile.InitialiseTile(n);

        heldTile = null;
        heldItemUI.sprite = null;
        heldItemUI.color = Color.clear;
    }


    private void SpriteSwap()
    {
        anim.SetFloat("X", playerDirection.x);
        anim.SetFloat("Y", playerDirection.y);
    }


    public IEnumerator WaterHang(Vector2 d)
    {
        hangReturnDirection = new Vector2(-d.x, -d.y);
        hanging = true;

        yield return new WaitForSeconds(hangTime);

        if (hanging)
            FallInWater();
    }

    private void FallInWater()
    {
        StartCoroutine(KillPlayer());
    }


    public IEnumerator KillPlayer()
    {
        inputDelay = 100;
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void QuestionReaction()
    {
        StartCoroutine(IEQuestionReaction());
    }
    public void ExclamationReaction()
    {
        StartCoroutine(IEExclamationReaction());
    }

    private IEnumerator IEQuestionReaction()
    {
        exclamationReaction.SetActive(false);
        questionReaction.SetActive(true);
        yield return new WaitForSeconds(reactionTime);
        questionReaction.SetActive(false);
    }

    private IEnumerator IEExclamationReaction()
    {
        exclamationReaction.SetActive(true);
        questionReaction.SetActive(false);
        yield return new WaitForSeconds(reactionTime);
        exclamationReaction.SetActive(false);
    }


    public override void InitialiseItem(Node n)
    {
        base.InitialiseItem(n);
    }
}
