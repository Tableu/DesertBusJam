using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.15,0.15,0,0,0,0]")]
	public partial class PlayerNetworkObject : NetworkObject
	{
		public const int IDENTITY = 10;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private Vector3 _position;
		public event FieldEvent<Vector3> positionChanged;
		public InterpolateVector3 positionInterpolation = new InterpolateVector3() { LerpT = 0.15f, Enabled = true };
		public Vector3 position
		{
			get { return _position; }
			set
			{
				// Don't do anything if the value is the same
				if (_position == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_position = value;
				hasDirtyFields = true;
			}
		}

		public void SetpositionDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_position(ulong timestep)
		{
			if (positionChanged != null) positionChanged(_position, timestep);
			if (fieldAltered != null) fieldAltered("position", _position, timestep);
		}
		[ForgeGeneratedField]
		private Quaternion _rotation;
		public event FieldEvent<Quaternion> rotationChanged;
		public InterpolateQuaternion rotationInterpolation = new InterpolateQuaternion() { LerpT = 0.15f, Enabled = true };
		public Quaternion rotation
		{
			get { return _rotation; }
			set
			{
				// Don't do anything if the value is the same
				if (_rotation == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_rotation = value;
				hasDirtyFields = true;
			}
		}

		public void SetrotationDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_rotation(ulong timestep)
		{
			if (rotationChanged != null) rotationChanged(_rotation, timestep);
			if (fieldAltered != null) fieldAltered("rotation", _rotation, timestep);
		}
		[ForgeGeneratedField]
		private int _score;
		public event FieldEvent<int> scoreChanged;
		public Interpolated<int> scoreInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int score
		{
			get { return _score; }
			set
			{
				// Don't do anything if the value is the same
				if (_score == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_score = value;
				hasDirtyFields = true;
			}
		}

		public void SetscoreDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_score(ulong timestep)
		{
			if (scoreChanged != null) scoreChanged(_score, timestep);
			if (fieldAltered != null) fieldAltered("score", _score, timestep);
		}
		[ForgeGeneratedField]
		private int _spriteIndex;
		public event FieldEvent<int> spriteIndexChanged;
		public Interpolated<int> spriteIndexInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int spriteIndex
		{
			get { return _spriteIndex; }
			set
			{
				// Don't do anything if the value is the same
				if (_spriteIndex == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x8;
				_spriteIndex = value;
				hasDirtyFields = true;
			}
		}

		public void SetspriteIndexDirty()
		{
			_dirtyFields[0] |= 0x8;
			hasDirtyFields = true;
		}

		private void RunChange_spriteIndex(ulong timestep)
		{
			if (spriteIndexChanged != null) spriteIndexChanged(_spriteIndex, timestep);
			if (fieldAltered != null) fieldAltered("spriteIndex", _spriteIndex, timestep);
		}
		[ForgeGeneratedField]
		private int _sortingOrder;
		public event FieldEvent<int> sortingOrderChanged;
		public Interpolated<int> sortingOrderInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int sortingOrder
		{
			get { return _sortingOrder; }
			set
			{
				// Don't do anything if the value is the same
				if (_sortingOrder == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x10;
				_sortingOrder = value;
				hasDirtyFields = true;
			}
		}

		public void SetsortingOrderDirty()
		{
			_dirtyFields[0] |= 0x10;
			hasDirtyFields = true;
		}

		private void RunChange_sortingOrder(ulong timestep)
		{
			if (sortingOrderChanged != null) sortingOrderChanged(_sortingOrder, timestep);
			if (fieldAltered != null) fieldAltered("sortingOrder", _sortingOrder, timestep);
		}
		[ForgeGeneratedField]
		private int _playerCharacter;
		public event FieldEvent<int> playerCharacterChanged;
		public Interpolated<int> playerCharacterInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int playerCharacter
		{
			get { return _playerCharacter; }
			set
			{
				// Don't do anything if the value is the same
				if (_playerCharacter == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x20;
				_playerCharacter = value;
				hasDirtyFields = true;
			}
		}

		public void SetplayerCharacterDirty()
		{
			_dirtyFields[0] |= 0x20;
			hasDirtyFields = true;
		}

		private void RunChange_playerCharacter(ulong timestep)
		{
			if (playerCharacterChanged != null) playerCharacterChanged(_playerCharacter, timestep);
			if (fieldAltered != null) fieldAltered("playerCharacter", _playerCharacter, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			positionInterpolation.current = positionInterpolation.target;
			rotationInterpolation.current = rotationInterpolation.target;
			scoreInterpolation.current = scoreInterpolation.target;
			spriteIndexInterpolation.current = spriteIndexInterpolation.target;
			sortingOrderInterpolation.current = sortingOrderInterpolation.target;
			playerCharacterInterpolation.current = playerCharacterInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _position);
			UnityObjectMapper.Instance.MapBytes(data, _rotation);
			UnityObjectMapper.Instance.MapBytes(data, _score);
			UnityObjectMapper.Instance.MapBytes(data, _spriteIndex);
			UnityObjectMapper.Instance.MapBytes(data, _sortingOrder);
			UnityObjectMapper.Instance.MapBytes(data, _playerCharacter);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_position = UnityObjectMapper.Instance.Map<Vector3>(payload);
			positionInterpolation.current = _position;
			positionInterpolation.target = _position;
			RunChange_position(timestep);
			_rotation = UnityObjectMapper.Instance.Map<Quaternion>(payload);
			rotationInterpolation.current = _rotation;
			rotationInterpolation.target = _rotation;
			RunChange_rotation(timestep);
			_score = UnityObjectMapper.Instance.Map<int>(payload);
			scoreInterpolation.current = _score;
			scoreInterpolation.target = _score;
			RunChange_score(timestep);
			_spriteIndex = UnityObjectMapper.Instance.Map<int>(payload);
			spriteIndexInterpolation.current = _spriteIndex;
			spriteIndexInterpolation.target = _spriteIndex;
			RunChange_spriteIndex(timestep);
			_sortingOrder = UnityObjectMapper.Instance.Map<int>(payload);
			sortingOrderInterpolation.current = _sortingOrder;
			sortingOrderInterpolation.target = _sortingOrder;
			RunChange_sortingOrder(timestep);
			_playerCharacter = UnityObjectMapper.Instance.Map<int>(payload);
			playerCharacterInterpolation.current = _playerCharacter;
			playerCharacterInterpolation.target = _playerCharacter;
			RunChange_playerCharacter(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _position);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _rotation);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _score);
			if ((0x8 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _spriteIndex);
			if ((0x10 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _sortingOrder);
			if ((0x20 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _playerCharacter);

			// Reset all the dirty fields
			for (int i = 0; i < _dirtyFields.Length; i++)
				_dirtyFields[i] = 0;

			return dirtyFieldsData;
		}

		protected override void ReadDirtyFields(BMSByte data, ulong timestep)
		{
			if (readDirtyFlags == null)
				Initialize();

			Buffer.BlockCopy(data.byteArr, data.StartIndex(), readDirtyFlags, 0, readDirtyFlags.Length);
			data.MoveStartIndex(readDirtyFlags.Length);

			if ((0x1 & readDirtyFlags[0]) != 0)
			{
				if (positionInterpolation.Enabled)
				{
					positionInterpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					positionInterpolation.Timestep = timestep;
				}
				else
				{
					_position = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_position(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (rotationInterpolation.Enabled)
				{
					rotationInterpolation.target = UnityObjectMapper.Instance.Map<Quaternion>(data);
					rotationInterpolation.Timestep = timestep;
				}
				else
				{
					_rotation = UnityObjectMapper.Instance.Map<Quaternion>(data);
					RunChange_rotation(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (scoreInterpolation.Enabled)
				{
					scoreInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					scoreInterpolation.Timestep = timestep;
				}
				else
				{
					_score = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_score(timestep);
				}
			}
			if ((0x8 & readDirtyFlags[0]) != 0)
			{
				if (spriteIndexInterpolation.Enabled)
				{
					spriteIndexInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					spriteIndexInterpolation.Timestep = timestep;
				}
				else
				{
					_spriteIndex = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_spriteIndex(timestep);
				}
			}
			if ((0x10 & readDirtyFlags[0]) != 0)
			{
				if (sortingOrderInterpolation.Enabled)
				{
					sortingOrderInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					sortingOrderInterpolation.Timestep = timestep;
				}
				else
				{
					_sortingOrder = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_sortingOrder(timestep);
				}
			}
			if ((0x20 & readDirtyFlags[0]) != 0)
			{
				if (playerCharacterInterpolation.Enabled)
				{
					playerCharacterInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					playerCharacterInterpolation.Timestep = timestep;
				}
				else
				{
					_playerCharacter = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_playerCharacter(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (positionInterpolation.Enabled && !positionInterpolation.current.UnityNear(positionInterpolation.target, 0.0015f))
			{
				_position = (Vector3)positionInterpolation.Interpolate();
				//RunChange_position(positionInterpolation.Timestep);
			}
			if (rotationInterpolation.Enabled && !rotationInterpolation.current.UnityNear(rotationInterpolation.target, 0.0015f))
			{
				_rotation = (Quaternion)rotationInterpolation.Interpolate();
				//RunChange_rotation(rotationInterpolation.Timestep);
			}
			if (scoreInterpolation.Enabled && !scoreInterpolation.current.UnityNear(scoreInterpolation.target, 0.0015f))
			{
				_score = (int)scoreInterpolation.Interpolate();
				//RunChange_score(scoreInterpolation.Timestep);
			}
			if (spriteIndexInterpolation.Enabled && !spriteIndexInterpolation.current.UnityNear(spriteIndexInterpolation.target, 0.0015f))
			{
				_spriteIndex = (int)spriteIndexInterpolation.Interpolate();
				//RunChange_spriteIndex(spriteIndexInterpolation.Timestep);
			}
			if (sortingOrderInterpolation.Enabled && !sortingOrderInterpolation.current.UnityNear(sortingOrderInterpolation.target, 0.0015f))
			{
				_sortingOrder = (int)sortingOrderInterpolation.Interpolate();
				//RunChange_sortingOrder(sortingOrderInterpolation.Timestep);
			}
			if (playerCharacterInterpolation.Enabled && !playerCharacterInterpolation.current.UnityNear(playerCharacterInterpolation.target, 0.0015f))
			{
				_playerCharacter = (int)playerCharacterInterpolation.Interpolate();
				//RunChange_playerCharacter(playerCharacterInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public PlayerNetworkObject() : base() { Initialize(); }
		public PlayerNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public PlayerNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
