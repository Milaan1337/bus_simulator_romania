using Godot;
using System;

public class Cars : KinematicBody2D
{
    [Export] private float _steeringAngle = 15.0f;
    [Export] private int _enginePoser = 400;
    [Export] private float _maxSpeedReverse = 250.0f;
    [Export] private string type;
    public string forward_key;
    public string backward_key;
    public string left_key;
    public string right_key;
    private float _wheelBase = 70.0f;

    private Vector2 _velocity = Vector2.Zero;
    private float _steerAngle;
    private Vector2 _acceleration = Vector2.Zero;
    private float _friction = -0.9f;
    private float _drag = -0.0015f;
    private float _breaking = -450.0f;

    private float _slipSpeed = 10;
    private float _tractionFast = 0.1f;
    private float _tractionSlow = 0.7f;
    public AllVariable allVariable = new AllVariable();
    public Sprite carsprite;
    public TextureProgress nitrousbar;
    public Sprite secondcarsprite;
    public TextureProgress secondnitrousbar;
    public int first;
	public int second;
    public int shake = 1;
    public Random rnd;
	public Camera2D camera;
    private bool allowInput = true;


    public override void _Ready()
    {
        if (allVariable.singleplay == true)
        {
            carsprite = GetNode("/root/Game/Car/KinematicBody2D/Sprite") as Sprite; 
            nitrousbar = GetNode("/root/Game/Car/HUD/NitrousBar") as TextureProgress; 
            camera = GetNode("/root/Game/Car/KinematicBody2D/Camera2D") as Camera2D;
        }
        else
        {
            carsprite = GetNode("/root/Game/VBoxContainer/ViewportContainer/Viewport/Cars/KinematicBody2D/Sprite") as Sprite; 
            nitrousbar = GetNode("/root/Game/VBoxContainer/ViewportContainer/HUD/NitrousBar") as TextureProgress;
            secondcarsprite = GetNode("/root/Game/VBoxContainer/ViewportContainer/Viewport/Cars/KinematicBody2D/Sprite") as Sprite;
            secondnitrousbar = GetNode("/root/Game/VBoxContainer/ViewportContainer2/HUD/NitrousBar") as TextureProgress;
            camera = GetNode("/root/Game/VBoxContainer/ViewportContainer/Viewport/Camera2D") as Camera2D;
        }
        
    }



    public void ToggleInput()
    {
        allowInput = false;
    }




    public override void _PhysicsProcess(float delta)
    {
        _acceleration = Vector2.Zero;

        GetInput();
        ApplyFriction();
        CalculateSteering(delta);

        _velocity += _acceleration * delta;
        _velocity = MoveAndSlide(_velocity);
    }

    private void GetInput()
    {
        if (!allowInput)
        {
            return;
        }

        float turn = Input.GetActionStrength("right") - Input.GetActionStrength("left");
        _steerAngle = turn * Mathf.Deg2Rad(_steeringAngle);

        if (Input.IsActionPressed("forward"))
        {
            _acceleration = Transform.x * _enginePoser;
        }
        if (Input.IsActionPressed("backward"))
        {
            _acceleration = Transform.x * _breaking;
        }

        if (Input.IsActionPressed("nitrous") && allVariable.nitrous >= 1)
		{
				carsprite.Texture = (Texture)ResourceLoader.Load("res://assets/Images/nitrouscar.png");
				allVariable.nitrous--;
				nitrousbar.Value = allVariable.nitrous;
				allVariable.speed = 1500;
				rnd = new Random();
				first = rnd.Next(-5, 5);
				second = rnd.Next(-5, 5);
				camera.SetOffset(new Vector2(first * shake, second * shake));
		}
		else
		{
			carsprite.Texture = (Texture)ResourceLoader.Load("res://assets/Images/car.png");
            allVariable.speed = 400;
		}
    }


    private void CalculateSteering(float delta)
    {
        Vector2 rearWheel = Position - Transform.x * _wheelBase / 4.0f;
        Vector2 frontWheel = Position + Transform.x * _wheelBase / 4.0f;
        rearWheel += _velocity * delta;
        frontWheel += _velocity.Rotated(_steerAngle) * delta;
        Vector2 newHeading = (frontWheel - rearWheel).Normalized();

        float traction = _tractionSlow;
        if (_velocity.Length() > _slipSpeed)
            traction = _tractionFast;


        float d = newHeading.Dot(_velocity.Normalized());
        if (d < 0)
            _velocity = -newHeading * Mathf.Min(_velocity.Length(), _maxSpeedReverse);
        else if (d > 0)
            _velocity = _velocity.LinearInterpolate(newHeading * _velocity.Length(), traction);

        Rotation = newHeading.Angle();
    }



    private void ApplyFriction()
    {
        if (_velocity.Length() < 5)
            _velocity = Vector2.Zero;

        Vector2 frictionForce = _velocity * _friction;
        Vector2 dragForce = _velocity * _velocity.Length() * _drag;

        if (_velocity.Length() < 100)
            frictionForce *= 3;

        _acceleration += dragForce + frictionForce;
    }
    
    public override void _Process(float delta)
	{
        allVariable = new AllVariable();
        _enginePoser = allVariable.speed;
    }
}
