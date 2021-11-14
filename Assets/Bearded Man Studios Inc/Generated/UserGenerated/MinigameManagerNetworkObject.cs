using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0]")]
	public partial class MinigameManagerNetworkObject : NetworkObject
	{
		public const int IDENTITY = 4;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private int _PlayerCount;
		public event FieldEvent<int> PlayerCountChanged;
		public Interpolated<int> PlayerCountInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int PlayerCount
		{
			get { return _PlayerCount; }
			set
			{
				// Don't do anything if the value is the same
				if (_PlayerCount == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_PlayerCount = value;
				hasDirtyFields = true;
			}
		}

		public void SetPlayerCountDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_PlayerCount(ulong timestep)
		{
			if (PlayerCountChanged != null) PlayerCountChanged(_PlayerCount, timestep);
			if (fieldAltered != null) fieldAltered("PlayerCount", _PlayerCount, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			PlayerCountInterpolation.current = PlayerCountInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _PlayerCount);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_PlayerCount = UnityObjectMapper.Instance.Map<int>(payload);
			PlayerCountInterpolation.current = _PlayerCount;
			PlayerCountInterpolation.target = _PlayerCount;
			RunChange_PlayerCount(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _PlayerCount);

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
				if (PlayerCountInterpolation.Enabled)
				{
					PlayerCountInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					PlayerCountInterpolation.Timestep = timestep;
				}
				else
				{
					_PlayerCount = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_PlayerCount(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (PlayerCountInterpolation.Enabled && !PlayerCountInterpolation.current.UnityNear(PlayerCountInterpolation.target, 0.0015f))
			{
				_PlayerCount = (int)PlayerCountInterpolation.Interpolate();
				//RunChange_PlayerCount(PlayerCountInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public MinigameManagerNetworkObject() : base() { Initialize(); }
		public MinigameManagerNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public MinigameManagerNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
