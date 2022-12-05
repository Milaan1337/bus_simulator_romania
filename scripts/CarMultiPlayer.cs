using Godot;
using System;

public class CarMultiPlayer : KinematicBody2D
{
    [Export] private float _steeringAngle = 15.0f;
    [Export] private int _enginePoser = 400;
    [Export] private float _maxSpeedReverse = 250.0f;

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
    public AllVariable allVariable;
    private bool allowInput = true;
    public Sprite carsprite;
    public TextureProgress nitrousbar;
    public Camera2D camera;
    public int first;
    public int second;
    public int shake = 1;
    public Random rnd;

    public override void _Ready()
    {
        allVariable = new AllVariable();
        carsprite = GetNode("/root/Game/VBoxContainer/ViewportContainer2/Viewport/CarMulti/KinematicBody2D/Sprite") as Sprite;
        nitrousbar = GetNode("/root/Game/VBoxContainer/ViewportContainer/HUD/NitrousBar") as TextureProgress;
        camera = GetNode("/root/Game/VBoxContainer/ViewportContainer/Viewport/Camera2D") as Camera2D;
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

        float turn = Input.GetActionStrength("secondleft") - Input.GetActionStrength("secondright");
        _steerAngle = turn * Mathf.Deg2Rad(_steeringAngle);

        if (Input.IsActionPressed("secondforward"))
        {
            _acceleration = Transform.x * _enginePoser;
        }
        if (Input.IsActionPressed("secondbackward"))
        {
            _acceleration = Transform.x * _breaking;
        }

        if (Input.IsActionPressed("nitrous2") && allVariable.nitrous2 >= 1)
		{
				carsprite.Texture = (Texture)ResourceLoader.Load("res://assets/Images/nitrouscar.png");
				allVariable.nitrous2--;
				nitrousbar.Value = allVariable.nitrous2;
				allVariable.speed = 1500;
				rnd = new Random();
				first = rnd.Next(-5, 5);
				second = rnd.Next(-5, 5);
				camera.SetOffset(new Vector2(first * shake, second * shake));
		}
		else
		{
			carsprite.Texture = (Texture)ResourceLoader.Load("res://assets/Images/Car1.png");
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
        //talan ide kell
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
