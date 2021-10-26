using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Energy.Contracts.Requests;
using Energy.Contracts.Responses;
using System.Linq;
using Energy.Contracts.DTOs;

namespace Energy.Handlers.ProductionPlan
{
    public class CalculateUnitCommitmentHandler : IHandler<SituationRequest, ObjectResult>
    {
        public ObjectResult Handle(SituationRequest request)
        {
            List<UnitCommitment> ToReturn = new();
            foreach (var plant in request.PowerPlants)
            {
                ToReturn.Add(new UnitCommitment
                {
                    Name = plant.Name,
                    P = 0
                });
            }

            int powerToReachRemaining = request.Load;

            if (powerToReachRemaining <= 0)
            {
                return new OkObjectResult(ToReturn);
            }

            List<PowerPlantDTO> powerPlants = request.PowerPlants
                        .OrderByDescending(plant => plant.Efficiency)
                        .ToList();

            int windEfficiency = request.Fuels.Wind;

            this.CalculOptimalElectricityProduction(powerPlants, ref powerToReachRemaining, ref ToReturn, windEfficiency);

            return new OkObjectResult(ToReturn);
        }

        public void CalculOptimalElectricityProduction(List<PowerPlantDTO> powerPlants, ref int powerToReachRemaining, ref List<UnitCommitment> ToReturn, int windEfficiency)
        {
            for (int index = 0; index < powerPlants.Count() - 1; index++)
            {
                PowerPlantDTO selectedPowerPlants = powerPlants[index];
                int powerAdjusted;
                for (int power = selectedPowerPlants.Pmax; power >= selectedPowerPlants.Pmin; power--)
                {
                    powerAdjusted = power;
                    if (selectedPowerPlants.Type == PowerPlantType.WindTurbine)
                    {
                        powerAdjusted = this.CalculWindPower(power, windEfficiency);
                    }
                    if (powerAdjusted > 0 && powerAdjusted <= powerToReachRemaining)
                    {
                        int indexToReplace;
                        if (index < powerPlants.Count())
                        {
                            if (!(powerPlants[index + 1].Pmin > powerToReachRemaining - powerAdjusted))
                            {
                                indexToReplace = ToReturn.FindIndex(plant => plant.Name == selectedPowerPlants.Name);
                                ToReturn[indexToReplace] = new UnitCommitment
                                {
                                    Name = selectedPowerPlants.Name,
                                    P = powerAdjusted
                                };
                                powerToReachRemaining -= powerAdjusted;
                                break;
                            }
                        }
                        else
                        {
                            indexToReplace = ToReturn.FindIndex(plant => plant.Name == selectedPowerPlants.Name);
                            ToReturn[indexToReplace] = new UnitCommitment
                            {
                                Name = selectedPowerPlants.Name,
                                P = powerAdjusted
                            };
                            powerToReachRemaining -= powerAdjusted;
                            break;
                        }
                    }
                }

                if (powerToReachRemaining == 0)
                {
                    break;
                }
            }
        }

        public int CalculWindPower(int power, int windEfficiency)
        {
            return (int)(power * (windEfficiency / 100.0));
        }
    }
}
