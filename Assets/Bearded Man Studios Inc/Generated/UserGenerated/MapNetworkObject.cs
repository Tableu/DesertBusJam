using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0]")]
	public partial class MapNetworkObject : NetworkObject
	{
		public const int IDENTITY = 4;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
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
				_dirtyFields[0] |= 0x1;
				_score = value;
				hasDirtyFields = true;
			}
		}

		public void SetscoreDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_score(ulong timestep)
		{
			if (scoreChanged != null) scoreChanged(_score, timestep);
			if (fieldAltered != null) fieldAltered("score", _score, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			scoreInterpolation.current = scoreInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _score);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_score = UnityObjectMapper.Instance.Map<int>(payload);
			scoreInterpolation.current = _score;
			scoreInterpolation.target = _score;
			RunChange_score(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _score);

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
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (scoreInterpolation.Enabled && !scoreInterpolation.current.UnityNear(scoreInterpolation.target, 0.0015f))
			{
				_score = (int)scoreInterpolation.Interpolate();
				//RunChange_score(scoreInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public MapNetworkObject() : base() { Initialize(); }
		public MapNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public MapNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
