using UnityEngine;

public class SausageMovement
{
    private readonly Rigidbody _rb;
    private readonly float _moveSpeed;
    private readonly float _rotationSpeed;
    private bool _isAutoRotating;
    private ObstacleObjects _obstacle;
    private bool _canMove;

    public SausageMovement(Rigidbody rb, float moveSpeed, float rotationSpeed)
    {
        _rb = rb;
        _moveSpeed = moveSpeed;
        _rotationSpeed = rotationSpeed;
    }
    
    public void Move(Vector2 input)
    {
        if (!_canMove) return;
       
        if (!_isAutoRotating)
        {
            var move = new Vector3(input.x, 0, input.y);
            _rb.MovePosition(_rb.position + move * (_moveSpeed * Time.fixedDeltaTime));
            if (!(move.sqrMagnitude > 0.01f)) return;
            var targetRot = Quaternion.LookRotation(move);
            _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, targetRot, _rotationSpeed * Time.fixedDeltaTime));
        }
        else
        {
            var move = new Vector3(0, 0, -input.y);
            var moveDir = _rb.transform.forward * move.z;

            _rb.MovePosition(_rb.position + moveDir * (_moveSpeed * Time.fixedDeltaTime));
        }
    }

    public void AutoRotate(ObstacleObjects obstacleObjects)
    {
        switch (obstacleObjects)
        {
            case ObstacleObjects.None:
                _rb.rotation = Quaternion.Euler(0, -90, 0);
                _isAutoRotating = false;
                break;
            case ObstacleObjects.RightWall:
                _isAutoRotating = true;
                _rb.rotation = Quaternion.Euler(0, -90, 0);
                break;
            case ObstacleObjects.LeftWall:
                _isAutoRotating = true;
                _rb.rotation = Quaternion.Euler(0, 90, 0);
                break;
          
        }
    }
    
    public void SetCanMove(bool canMove) => _canMove = canMove;
    
}