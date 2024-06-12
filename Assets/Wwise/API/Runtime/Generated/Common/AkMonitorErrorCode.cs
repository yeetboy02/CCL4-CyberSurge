#if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.
//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.1.1
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public enum AkMonitorErrorCode {
  ErrorCode_NoError = 0,
  ErrorCode_FileNotFound,
  ErrorCode_CannotOpenFile,
  ErrorCode_CannotStartStreamNoMemory,
  ErrorCode_IODevice,
  ErrorCode_IncompatibleIOSettings,
  ErrorCode_PluginUnsupportedChannelConfiguration,
  ErrorCode_PluginMediaUnavailable,
  ErrorCode_PluginInitialisationFailed,
  ErrorCode_PluginProcessingFailed,
  ErrorCode_PluginExecutionInvalid,
  ErrorCode_PluginAllocationFailed,
  ErrorCode_VorbisSeekTableRecommended,
  ErrorCode_VorbisDecodeError,
  ErrorCode_ATRAC9DecodeFailed,
  ErrorCode_ATRAC9LoopSectionTooSmall,
  ErrorCode_InvalidAudioFileHeader,
  ErrorCode_AudioFileHeaderTooLarge,
  ErrorCode_LoopTooSmall,
  ErrorCode_TransitionNotAccurateChannel,
  ErrorCode_TransitionNotAccuratePluginMismatch,
  ErrorCode_TransitionNotAccurateRejectedByPlugin,
  ErrorCode_TransitionNotAccurateStarvation,
  ErrorCode_TransitionNotAccurateCodecError,
  ErrorCode_NothingToPlay,
  ErrorCode_PlayFailed,
  ErrorCode_StingerCouldNotBeScheduled,
  ErrorCode_TooLongSegmentLookAhead,
  ErrorCode_CannotScheduleMusicSwitch,
  ErrorCode_TooManySimultaneousMusicSegments,
  ErrorCode_PlaylistStoppedForEditing,
  ErrorCode_MusicClipsRescheduledAfterTrackEdit,
  ErrorCode_CannotPlaySource_Create,
  ErrorCode_CannotPlaySource_VirtualOff,
  ErrorCode_CannotPlaySource_TimeSkip,
  ErrorCode_CannotPlaySource_InconsistentState,
  ErrorCode_MediaNotLoaded,
  ErrorCode_VoiceStarving,
  ErrorCode_StreamingSourceStarving,
  ErrorCode_XMADecoderSourceStarving,
  ErrorCode_XMADecodingError,
  ErrorCode_InvalidXMAData,
  ErrorCode_PluginNotRegistered,
  ErrorCode_CodecNotRegistered,
  ErrorCode_PluginVersionMismatch,
  ErrorCode_EventIDNotFound,
  ErrorCode_InvalidGroupID,
  ErrorCode_SelectedNodeNotAvailable,
  ErrorCode_SelectedMediaNotAvailable,
  ErrorCode_NoValidSwitch,
  ErrorCode_BankLoadFailed,
  ErrorCode_ErrorWhileLoadingBank,
  ErrorCode_InsufficientSpaceToLoadBank,
  ErrorCode_LowerEngineCommandListFull,
  ErrorCode_SeekNoMarker,
  ErrorCode_CannotSeekContinuous,
  ErrorCode_SeekAfterEof,
  ErrorCode_UnknownGameObject,
  ErrorCode_GameObjectNeverRegistered,
  ErrorCode_DeadGameObject,
  ErrorCode_GameObjectIsNotEmitter,
  ErrorCode_ExternalSourceNotResolved,
  ErrorCode_FileFormatMismatch,
  ErrorCode_CommandQueueFull,
  ErrorCode_CommandTooLarge,
  ErrorCode_XMACreateDecoderLimitReached,
  ErrorCode_XMAStreamBufferTooSmall,
  ErrorCode_ModulatorScopeError_Inst,
  ErrorCode_ModulatorScopeError_Obj,
  ErrorCode_SeekAfterEndOfPlaylist,
  ErrorCode_OpusRequireSeekTable,
  ErrorCode_OpusDecodeError,
  ErrorCode_SourcePluginNotFound,
  ErrorCode_VirtualVoiceLimit,
  ErrorCode_NotEnoughMemoryToStart,
  ErrorCode_UnknownOpusError,
  ErrorCode_AudioDeviceInitFailure,
  ErrorCode_AudioDeviceRemoveFailure,
  ErrorCode_AudioDeviceNotFound,
  ErrorCode_AudioDeviceNotValid,
  ErrorCode_SpatialAudio_ListenerAutomationNotSupported,
  ErrorCode_MediaDuplicationLength,
  ErrorCode_HwVoicesSystemInitFailed,
  ErrorCode_HwVoicesDecodeBatchFailed,
  ErrorCode_HwVoiceLimitReached,
  ErrorCode_HwVoiceInitFailed,
  ErrorCode_OpusHWCommandFailed,
  ErrorCode_AddOutputListenerIdWithZeroListeners,
  ErrorCode_3DObjectLimitExceeded,
  ErrorCode_OpusHWFatalError,
  ErrorCode_OpusHWDecodeUnavailable,
  ErrorCode_OpusHWTimeout,
  ErrorCode_SystemAudioObjectsUnavailable,
  ErrorCode_AddOutputNoDistinctListener,
  ErrorCode_PluginCannotRunOnObjectConfig,
  ErrorCode_SpatialAudio_ReflectionBusError,
  ErrorCode_VorbisHWDecodeUnavailable,
  ErrorCode_ExternalSourceNoMemorySize,
  ErrorCode_MonitorQueueFull,
  ErrorCode_MonitorMsgTooLarge,
  ErrorCode_NonCompliantDeviceMemory,
  ErrorCode_JobWorkerFuncCallMismatch,
  ErrorCode_JobMgrOutOfMemory,
  ErrorCode_InvalidFileSize,
  ErrorCode_PluginMsg,
  ErrorCode_SinkOpenSL,
  ErrorCode_AudioOutOfRange,
  ErrorCode_AudioOutOfRangeOnBus,
  ErrorCode_AudioOutOfRangeOnBusFx,
  ErrorCode_AudioOutOfRangeRay,
  ErrorCode_UnknownDialogueEvent,
  ErrorCode_FailedPostingEvent,
  ErrorCode_OutputDeviceInitializationFailed,
  ErrorCode_UnloadBankFailed,
  ErrorCode_PluginFileNotFound,
  ErrorCode_PluginFileIncompatible,
  ErrorCode_PluginFileNotEnoughMemoryToStart,
  ErrorCode_PluginFileInvalid,
  ErrorCode_PluginFileRegisterFailed,
  ErrorCode_UnknownArgument,
  ErrorCode_DynamicSequenceAlreadyClosed,
  ErrorCode_PendingActionDestroyed,
  ErrorCode_CrossFadeTransitionIgnored,
  ErrorCode_MusicRendererSeekingFailed,
  ErrorCode_DynamicSequenceIdNotFound,
  ErrorCode_BusNotFoundByName,
  ErrorCode_AudioDeviceShareSetNotFound,
  ErrorCode_AudioDeviceShareSetNotFoundByName,
  ErrorCode_SoundEngineTooManyGameObjects,
  ErrorCode_SoundEngineTooManyPositions,
  ErrorCode_SoundEngineCantCallOnChildBus,
  ErrorCode_SoundEnginePlayingIdNotFound,
  ErrorCode_SoundEngineInvalidTransform,
  ErrorCode_SoundEngineTooManyEventPosts,
  ErrorCode_AudioSubsystemStoppedResponding,
  ErrorCode_NotEnoughMemInFunction,
  ErrorCode_FXNotFound,
  ErrorCode_SetMixerNotABus,
  ErrorCode_AudioNodeNotFound,
  ErrorCode_SetMixerFailed,
  ErrorCode_SetBusConfigUnsupported,
  ErrorCode_BusNotFound,
  ErrorCode_MismatchingMediaSize,
  ErrorCode_IncompatibleBankVersion,
  ErrorCode_UnexpectedPrepareGameSyncsCall,
  ErrorCode_MusicEngineNotInitialized,
  ErrorCode_LoadingBankMismatch,
  ErrorCode_MasterBusStructureNotLoaded,
  ErrorCode_TooManyChildren,
  ErrorCode_BankContainUneditableEffect,
  ErrorCode_MemoryAllocationFailed,
  ErrorCode_InvalidFloatPriority,
  ErrorCode_SoundLoadFailedInsufficientMemory,
  ErrorCode_NXDeviceRegistrationFailed,
  ErrorCode_MixPluginOnObjectBus,
  ErrorCode_XboxXMAVoiceResetFailed,
  ErrorCode_XboxACPMessage,
  ErrorCode_XboxFrameDropped,
  ErrorCode_XboxACPError,
  ErrorCode_XboxXMAFatalError,
  ErrorCode_MissingMusicNodeParent,
  ErrorCode_HardwareOpusDecoderError,
  ErrorCode_SetGeometryTooManyTriangleConnected,
  ErrorCode_SetGeometryTriangleTooLarge,
  ErrorCode_SetGeometryFailed,
  ErrorCode_RemovingGeometrySetFailed,
  ErrorCode_SetGeometryInstanceFailed,
  ErrorCode_RemovingGeometryInstanceFailed,
  ErrorCode_RevertingToDefaultAudioDevice,
  ErrorCode_RevertingToDummyAudioDevice,
  ErrorCode_AudioThreadSuspended,
  ErrorCode_AudioThreadResumed,
  ErrorCode_ResetPlaylistActionIgnoredGlobalScope,
  ErrorCode_ResetPlaylistActionIgnoredContinuous,
  ErrorCode_PlayingTriggerRateNotSupported,
  ErrorCode_SetGeometryTriangleIsSkipped,
  ErrorCode_SetGeometryInstanceInvalidTransform,
  ErrorCode_SetGameObjectRadiusSizeError,
  ErrorCode_SetPortalNonDistinctRoom,
  ErrorCode_SetPortalInvalidExtent,
  ErrorCode_SpatialAudio_PortalNotFound,
  ErrorCode_InvalidFloatInFunction,
  ErrorCode_FLTMAXNotSupported,
  ErrorCode_CannotInitializeAmbisonicChannelConfiguration,
  ErrorCode_CannotInitializePassthrough,
  ErrorCode_3DAudioUnsupportedSize,
  ErrorCode_AmbisonicNotAvailable,
  ErrorCode_NoAudioDevice,
  ErrorCode_Support,
  ErrorCode_ReplayMessage,
  ErrorCode_GameMessage,
  ErrorCode_TestMessage,
  ErrorCode_TranslatorStandardTagTest,
  ErrorCode_TranslatorWwiseTagTest,
  ErrorCode_TranslatorStringSizeTest,
  ErrorCode_InvalidParameter,
  ErrorCode_MaxAudioObjExceeded,
  ErrorCode_MMSNotEnabled,
  ErrorCode_NotEnoughSystemObj,
  ErrorCode_NotEnoughSystemObjWin,
  ErrorCode_TransitionNotAccurateSourceTooShort,
  ErrorCode_AlreadyInitialized,
  ErrorCode_WrongNumberOfArguments,
  ErrorCode_DataAlignement,
  ErrorCode_PluginMsgWithShareSet,
  ErrorCode_SoundEngineNotInit,
  ErrorCode_NoDefaultSwitch,
  ErrorCode_CantSetBoundSwitch,
  ErrorCode_IODeviceInitFailed,
  ErrorCode_SwitchListEmpty,
  ErrorCode_NoSwitchSelected,
  Num_ErrorCodes
}
#endif // #if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.