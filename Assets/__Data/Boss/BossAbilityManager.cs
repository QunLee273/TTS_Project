// using System.Collections.Generic;
// using UnityEngine;
//
// public class BossAbilityManager : MonoBehaviour
// {
//     [Header("Boss Ability Manager")]
//     [SerializeField] private List<BossAbilityBase> abilities = new List<BossAbilityBase>();
//     [SerializeField] private float globalCooldown = 1f;
//     private float globalTimer;
//
//     public bool IsCasting { get; private set; }
//
//     private void Awake()
//     {
//         abilities.AddRange(GetComponentsInChildren<BossAbilityBase>());
//     }
//
//     private void Update()
//     {
//         if (IsCasting || !BossCtrl.Instance.IsAlive) return; // dead thì không cast
//         globalTimer += Time.deltaTime;
//
//         if (globalTimer < globalCooldown) return;
//
//         BossAbilityBase next = GetNextAbility();
//         if (next != null && next.CanUse())
//         {
//             globalTimer = 0;
//             StartCoroutine(CastAbility(next));
//         }
//     }
//
//     private BossAbilityBase GetNextAbility()
//     {
//         BossAbilityBase best = null;
//         foreach (var ability in abilities)
//         {
//             if (ability.CanUse())
//             {
//                 if (best == null || ability.priority > best.priority)
//                     best = ability;
//             }
//         }
//         return best;
//     }
//
//     private System.Collections.IEnumerator CastAbility(BossAbilityBase ability)
//     {
//         IsCasting = true;
//         BossCtrl.Instance.isUsingAbility = true;
//
//         yield return ability.Execute();
//
//         BossCtrl.Instance.isUsingAbility = false;
//         IsCasting = false;
//     }
// }